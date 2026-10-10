/*
    Migration: Add server-side expiry for remember-me auth tokens.

    Safe to run more than once.
    Existing users, reservations, menus, items, tables, and feedback are not deleted.

    Important behavior:
    - Adds Users.AuthTokenExpiry as NULL so existing rows stay valid.
    - Clears old remember-me tokens that were created before expiry existed.
      This is the safest option because old tokens had no server-side lifetime.
    - Users can still login normally with email/password.

    If you prefer to keep old remember-me cookies alive for 7 more days,
    comment out the UPDATE that clears AuthToken and use the alternative UPDATE below.
*/

BEGIN TRY
    BEGIN TRANSACTION;

    IF COL_LENGTH('dbo.Users', 'AuthTokenExpiry') IS NULL
    BEGIN
        ALTER TABLE dbo.Users
        ADD AuthTokenExpiry DATETIME NULL;
    END;

    /*
        Recommended: invalidate old remember-me tokens that do not have an expiry.
        This does not delete users or change passwords.
        It only makes old "remember me" cookies ask the user to login again.
    */
    UPDATE dbo.Users
    SET AuthToken = NULL,
        AuthTokenExpiry = NULL
    WHERE AuthToken IS NOT NULL
      AND AuthTokenExpiry IS NULL;

    /*
        Alternative if you want to preserve existing remember-me cookies:

        UPDATE dbo.Users
        SET AuthTokenExpiry = DATEADD(DAY, 7, GETUTCDATE())
        WHERE AuthToken IS NOT NULL
          AND AuthTokenExpiry IS NULL;
    */

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
        ROLLBACK TRANSACTION;
    END;

    THROW;
END CATCH;
