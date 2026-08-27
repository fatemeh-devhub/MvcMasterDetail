CREATE   PROCEDURE [dbo].[Usp_UpdateOrder]
(
    @OrderJson NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        DECLARE @OrderId UNIQUEIDENTIFIER;
        DECLARE @CustomerId UNIQUEIDENTIFIER;
        DECLARE @SellerId UNIQUEIDENTIFIER;
        DECLARE @GuidKey UNIQUEIDENTIFIER;

        SELECT
            @OrderId    = TRY_CAST(JSON_VALUE(@OrderJson, '$.Id') AS UNIQUEIDENTIFIER),
            @GuidKey    = TRY_CAST(JSON_VALUE(@OrderJson, '$.GuidKey') AS UNIQUEIDENTIFIER),
            @CustomerId = TRY_CAST(JSON_VALUE(@OrderJson, '$.CustomerId') AS UNIQUEIDENTIFIER),
            @SellerId   = TRY_CAST(JSON_VALUE(@OrderJson, '$.SellerId') AS UNIQUEIDENTIFIER);

        -----------------------------------------------------
        -- Update Header
        -----------------------------------------------------

        UPDATE OrderHeader
        SET
            CustomerId = @CustomerId,
            SellerId   = @SellerId
        WHERE Id = @OrderId;

        -----------------------------------------------------
        -- Delete Old Details
        -----------------------------------------------------

        DELETE FROM OrderDetails
        WHERE OrderId = @OrderId;

        -----------------------------------------------------
        -- Insert New Details
        -----------------------------------------------------

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
            @OrderId,
            ParentGuid,
            ProductId,
            UnitPrice,
            Quantity,
            0
        FROM OPENJSON(@OrderJson, '$.OrderDetails')
        WITH
        (
            ParentGuid UNIQUEIDENTIFIER '$.ParentGuid',
            ProductId UNIQUEIDENTIFIER '$.ProductId',
            UnitPrice DECIMAL(18,2) '$.UnitPrice',
            Quantity INT '$.Quantity'
        );

        COMMIT TRANSACTION;

        SELECT @OrderId AS OrderId;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END