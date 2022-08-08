
use master;

-- ===============================================
-- Create SQL Login template for Windows Azure SQL Database
-- ===============================================

CREATE LOGIN cushman_one_backup 
	WITH PASSWORD = 'CushM@n2018.1'
GO


drop USER cushman_one_backup 

CREATE USER cushman_one_backup FROM LOGIN cushman_one_backup
GO



ALTER ROLE dbmanager ADD MEMBER cushman_one_backup;


CREATE USER cushman_one_backup FROM LOGIN cushman_one_backup;

GRANT ALTER ANY USER TO cushman_one_backup;


ALTER ROLE db_owner ADD MEMBER cushman_one_backup; 