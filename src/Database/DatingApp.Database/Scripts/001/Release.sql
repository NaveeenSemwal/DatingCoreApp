PRINT 'Deploy version 001 data scripts';
 
RAISERROR('2000 Initial Database script for master tables.sql', 1, 1)
:r ".\Data\2000 Initial Database script for master tables.sql"
GO