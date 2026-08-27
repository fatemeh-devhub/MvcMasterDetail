CREATE     PROCEDURE [dbo].[Usp_InsertOrder]
(
    @OrderJson NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        DECLARE @InsertedRows TABLE
        (
            OrderId UNIQUEIDENTIFIER,
            GuidKey UNIQUEIDENTIFIER
        );

        DECLARE @CustomerId UNIQUEIDENTIFIER;
        DECLARE @SellerId UNIQUEIDENTIFIER;
        DECLARE @GuidKey UNIQUEIDENTIFIER;

        SELECT
            @CustomerId = TRY_CAST(JSON_VALUE(@OrderJson, '$.CustomerId') AS UNIQUEIDENTIFIER),
            @SellerId   = TRY_CAST(JSON_VALUE(@OrderJson, '$.SellerId') AS UNIQUEIDENTIFIER),
            @GuidKey    = TRY_CAST(JSON_VALUE(@OrderJson, '$.GuidKey') AS UNIQUEIDENTIFIER);

        INSERT INTO OrderHeader
        (
            Id,
            GuidKey,
            CustomerId,
            SellerId,
            IsDeleted
        )
        OUTPUT
            INSERTED.Id,
            INSERTED.GuidKey
        INTO @InsertedRows
        (
            OrderId,
            GuidKey
        )
        VALUES
        (
            NEWID(),
            @GuidKey,
            @CustomerId,
            @SellerId,
            0
        );

        INSERT INTO OrderDetails
        (
            Id,
            OrderId,
            ParentGuid,
            ProductId,
            UnitPrice,
            Quantity,
            IsDeleted
        )
        SELECT
            NEWID(),
            H.OrderId,
            D.ParentGuid,
            D.ProductId,
            D.UnitPrice,
            D.Quantity,
            0
        FROM OPENJSON(@OrderJson, '$.OrderDetails')
        WITH
        (
            ParentGuid UNIQUEIDENTIFIER '$.ParentGuid',
            ProductId UNIQUEIDENTIFIER '$.ProductId',
            UnitPrice DECIMAL(18,2) '$.UnitPrice',
            Quantity INT '$.Quantity'
        ) AS D
        INNER JOIN @InsertedRows AS H
            ON D.ParentGuid = H.GuidKey;

        COMMIT TRANSACTION;

        SELECT TOP (1)
            OrderId
        FROM @InsertedRows;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END


