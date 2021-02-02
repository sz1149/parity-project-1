CREATE PROCEDURE dbo.ImportLocations
AS
SET XACT_ABORT, NOCOUNT ON
BEGIN TRY
    BEGIN TRANSACTION;

        -- inserts
        INSERT INTO dbo.Location(Id, RegionId, Name, Latitude, Longitude)
        SELECT src.Id, src.RegionId, src.Name, src.Latitude, src.Longitude
        FROM staging.Location src
        LEFT OUTER JOIN dbo.Location dest on src.Id = dest.Id
        WHERE dest.Id IS NULL

        -- clear table
        TRUNCATE TABLE staging.Location;

    COMMIT TRANSACTION;

END TRY
BEGIN CATCH
    IF @@trancount > 0 ROLLBACK TRANSACTION
    DECLARE @msg NVARCHAR(2048) = ERROR_MESSAGE()
    RAISERROR (@msg, 16, 1)
END CATCH;
go

