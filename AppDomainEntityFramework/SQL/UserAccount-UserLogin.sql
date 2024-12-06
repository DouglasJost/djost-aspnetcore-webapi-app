
-- SQL FALSE : 0
-- SQL TRUE  : 1

SELECT NEWID()

-- ==================================================

select * from UserAccount 

INSERT INTO UserAccount (UserAccountId, Inactive, FirstName, LastName, UserDefined, LastModifiedDate)
  VALUES ('4EC76740-6895-40F4-ABB8-3FBAB440FFF1', 0, 'JWT', 'Issuer', 1, GETUTCDATE())

-- ==================================================

select * from UserLogin 

INSERT INTO UserLogin (UserAccountId, Inactive, Login, Password, UserDefined, LastModifiedDate)
  VALUES ('4EC76740-6895-40F4-ABB8-3FBAB440FFF1', 0, 'Login', 'HelloGoodbye', 1, GETUTCDATE())


