
-- 
-- Delete Seed Data
--

USE MusicCollectionDB;
GO


BEGIN TRANSACTION;

BEGIN TRY
    PRINT '01 Checking if SongWriter entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM SongWriter)
    BEGIN
        DELETE FROM SongWriter;
        PRINT 'SongWriter entries deleted.'
    END;

    PRINT '02 Checking if BandMembership entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM BandMembership)
    BEGIN
        DELETE FROM BandMembership;
        PRINT 'BandMembership entries deleted.'
    END;

    PRINT '03 Checking if Song entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM Song)
    BEGIN
        DELETE FROM Song;
        PRINT 'Song entries deleted.'
    END;

    PRINT '04 Checking if Album entries need to be deleted.';
    IF EXISTS (SELECT 1 FROM Album)
    BEGIN
        DELETE FROM Album;
        PRINT 'Album entries deleted.'
    END;

    PRINT '05 Checking if Band entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM Band)
    BEGIN
        DELETE FROM Band;
        PRINT 'Band entries deleted.'
    END;

    PRINT '06 Checking if Artist entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM Artist)
    BEGIN
        DELETE FROM Artist;
        PRINT 'Artist entries deleted.'
    END;

    PRINT '07 Checking if Genre entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM Genre)
    BEGIN
        DELETE FROM Genre;
        PRINT 'Genre entries deleted.'
    END;
    -- Commit transaction
    PRINT 'Commit the transaction'
    COMMIT TRANSACTION;

    PRINT 'MusicCollection seed data was inserted successfully.'
END TRY
BEGIN CATCH
    -- Rollback the transaction because of an error
    PRINT 'Rollback the transaction because of an error.'
    ROLLBACK TRANSACTION;

    -- Display error
    PRINT 'An error occurred while inserting seed data.'
    PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH;
