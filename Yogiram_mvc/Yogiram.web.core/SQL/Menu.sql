



--insert into AT_Modules (ModuleId,ModuleName,Enable,CreatedDate,CreatedBy) values ('7F3BFDFD-633B-48AA-8A09-D2BD7330125E','Attendance',1,GETDATE(),'Sys-Admin')

--insert into AT_Modules (ModuleId,ModuleName,Enable,CreatedDate,CreatedBy) values ('367128CE-4D5A-4F1F-9181-15A96B5CC42C','EIM',1,GETDATE(),'Sys-Admin')

--insert into AT_Modules (ModuleId,ModuleName,Enable,CreatedDate,CreatedBy) values ('E0A7F194-CE40-4E9B-A5AD-59AC32BE00A9','Manage',1,GETDATE(),'Sys-Admin')


-- Menu for attendance
EXECUTE [dbsp_AT_AddMenu] @MenuId = 'D79DB074-8067-4084-A03A-E194F5A5827E', @ModuleId = '7F3BFDFD-633B-48AA-8A09-D2BD7330125E', @ParentMenuId = null, @MenuName = 'Master', @Url = null, @Sort = 1
GO
EXECUTE [dbsp_AT_AddMenu] @MenuId = '94D4A131-A441-46EC-B027-5363BF417233', @ModuleId = '7F3BFDFD-633B-48AA-8A09-D2BD7330125E', @ParentMenuId = 'D79DB074-8067-4084-A03A-E194F5A5827E', @MenuName = 'Home', @Url = '~/Attandance/Home', @Sort = 1
GO


-- Menu for EIM
EXECUTE [dbsp_AT_AddMenu] @MenuId = 'D168C4AB-AB3D-4BF3-B20A-F125B44CB20C', @ModuleId = '367128CE-4D5A-4F1F-9181-15A96B5CC42C', @ParentMenuId = null, @MenuName = 'Master', @Url = null, @Sort = 1
GO
EXECUTE [dbsp_AT_AddMenu] @MenuId = 'DA1066F0-9197-426E-8D89-B0BE918D36FE', @ModuleId = '367128CE-4D5A-4F1F-9181-15A96B5CC42C', @ParentMenuId = 'D168C4AB-AB3D-4BF3-B20A-F125B44CB20C', @MenuName = 'User Registration', @Url = '~/EIM/UserRegistration', @Sort = 1
GO


-- Menu for Manage
EXECUTE [dbsp_AT_AddMenu] @MenuId = 'F128AC08-9CAF-4A20-A244-5582C6DA32F2', @ModuleId = 'E0A7F194-CE40-4E9B-A5AD-59AC32BE00A9', @ParentMenuId = null, @MenuName = 'Master', @Url = null, @Sort = 1
GO

EXECUTE [dbsp_AT_AddMenu] @MenuId = '7EF5A940-0C11-4304-8964-65ECD865FEA5', @ModuleId = 'E0A7F194-CE40-4E9B-A5AD-59AC32BE00A9', @ParentMenuId = 'F128AC08-9CAF-4A20-A244-5582C6DA32F2', @MenuName = 'Company Registration', @Url = '~/Manage/CompanyRegistration', @Sort = 1
GO
EXECUTE [dbsp_AT_AddMenu] @MenuId = 'EDCB74EA-10B1-4EB4-9E7A-C0FBE9668D08', @ModuleId = 'E0A7F194-CE40-4E9B-A5AD-59AC32BE00A9', @ParentMenuId = 'F128AC08-9CAF-4A20-A244-5582C6DA32F2', @MenuName = 'Role Creation', @Url = '~/Manage/RoleCreation', @Sort = 2
GO


EXECUTE [dbsp_AT_AddMenu] @MenuId = '93BF5E2E-BD57-4490-A9AA-A22E6006127C', @ModuleId = 'E0A7F194-CE40-4E9B-A5AD-59AC32BE00A9', @ParentMenuId = null, @MenuName = 'Transaction', @Url = null, @Sort = 1
GO

EXECUTE [dbsp_AT_AddMenu] @MenuId = '8CE0ECD7-AE79-4288-8C38-B59FC75EEEFD', @ModuleId = 'E0A7F194-CE40-4E9B-A5AD-59AC32BE00A9', @ParentMenuId = '93BF5E2E-BD57-4490-A9AA-A22E6006127C', @MenuName = 'Menu Assign', @Url = '~/Manage/RoleCreation/MenuAssign', @Sort = 1
GO
EXECUTE [dbsp_AT_AddMenu] @MenuId = 'D8AD99EB-AC6D-44ED-8638-716582DF41EA', @ModuleId = 'E0A7F194-CE40-4E9B-A5AD-59AC32BE00A9', @ParentMenuId = '93BF5E2E-BD57-4490-A9AA-A22E6006127C', @MenuName = 'Roles for Menu', @Url = '~/Manage/RoleCreation/RolesInMenu', @Sort = 2
GO
EXECUTE [dbsp_AT_AddMenu] @MenuId = '7C7FA38C-E313-4DF2-89ED-D04A65BD4C9A', @ModuleId = 'E0A7F194-CE40-4E9B-A5AD-59AC32BE00A9', @ParentMenuId = '93BF5E2E-BD57-4490-A9AA-A22E6006127C', @MenuName = 'Roles for Modules', @Url = '~/Manage/RoleCreation/RolesInModules', @Sort = 3
GO
