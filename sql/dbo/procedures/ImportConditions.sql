CREATE PROCEDURE dbo.ImportConditions
AS
SET XACT_ABORT, NOCOUNT ON
BEGIN TRY
    BEGIN TRANSACTION;

        -- updates
        UPDATE dest
        SET Description = src.Description,
            Title = src.Title,
            Icon = src.Icon
        FROM staging.Condition src
        INNER JOIN dbo.Condition dest on src.Id = dest.Id

        -- inserts
        INSERT INTO dbo.Condition(Id, Description, Title, Icon)
        SELECT src.Id, src.Description, src.Title, src.Icon
        FROM staging.Condition src
        LEFT OUTER JOIN dbo.Condition dest on src.Id = dest.Id
        WHERE dest.Id IS NULL

        -- clear table
        TRUNCATE TABLE staging.Condition;

    COMMIT TRANSACTION;

END TRY
BEGIN CATCH
    IF @@trancount > 0 ROLLBACK TRANSACTION
    DECLARE @msg NVARCHAR(2048) = ERROR_MESSAGE()
    RAISERROR (@msg, 16, 1)
END CATCH;
go

