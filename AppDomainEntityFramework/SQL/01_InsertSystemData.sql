
-- 
-- Insert System (UserAccount and UserLogin) Entries
--
USE MusicCollectionDB;
GO


BEGIN TRANSACTION;

BEGIN TRY
    PRINT 'Checking if UserLogin entries need to be deleted.';
    IF EXISTS (SELECT 1 FROM UserLogin)
    BEGIN
        DELETE FROM UserLogin;
        PRINT 'UserLogin entries deleted.'
    END;

    PRINT 'Checking if UserAccount entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM UserAccount)
    BEGIN 
        DELETE FROM UserAccount;
        PRINT 'UserAccount entries deleted.'
    END;

    PRINT 'System UserAccount inserted successfully.'
    INSERT INTO UserAccount (UserAccountId, Inactive, FirstName, LastName, UserDefined, LastModifiedDate)
        VALUES ('4EC76740-6895-40F4-ABB8-3FBAB440FFF1', 0, 'JWT', 'Issuer', 1, GETUTCDATE())

    PRINT 'System UserLogin inserted successfully.'
    INSERT INTO UserLogin (UserAccountId, Inactive, Login, Password, UserDefined, LastModifiedDate)
        VALUES ('4EC76740-6895-40F4-ABB8-3FBAB440FFF1', 0, 'JwtIssuer', 'mVwmDVr8OwTwnbVwDvi40w==.DWy8ko+AwMzcA/yu2uGVVCiMM2dGdXkWmkn0FGZvkxk=', 1, GETUTCDATE())
  
    -- Commit transaction
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    -- Rollback the transaction because of an error
    ROLLBACK TRANSACTION;

    -- Display error
    PRINT 'An error occurred while inserting system and seed data.'
    PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH;

