CREATE   PROCEDURE [dbo].[Usp_SoftDeleteOrder]
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

        SELECT 
            @OrderId = TRY_CAST(JSON_VALUE(@OrderJson, '$.Id') AS UNIQUEIDENTIFIER);


        IF @OrderId IS NULL
        BEGIN
            RAISERROR('Invalid Order Id.', 16, 1);
            RETURN;
        END


        IF NOT EXISTS 
        (
            SELECT 1 
            FROM OrderHeader 
            WHERE Id = @OrderId
        )
        BEGIN
            RAISERROR('Order not found.', 16, 1);
            RETURN;
        END


        UPDATE OrderDetails
        SET 
            IsDeleted = 1
        WHERE OrderId = @OrderId;


        UPDATE OrderHeader
        SET 
            IsDeleted = 1
        WHERE Id = @OrderId;


        COMMIT TRANSACTION;


    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH

END
