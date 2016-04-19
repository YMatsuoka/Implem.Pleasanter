﻿using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.DataTypes;
using Implem.Pleasanter.Libraries.Utilities;
using System;
namespace Implem.Pleasanter.Libraries.Initializers
{
    public static class UsersInitializer
    {
        public static void Initialize()
        {
            if (Rds.ExecuteScalar_int(statements:
                Rds.SelectUsers(column: Rds.UsersColumn().UsersCount())) == 0)
            {
                Create(
                    1,
                    "Administrator",
                    "System Admin",
                    password: Securities.DefaultAdminPassword().Sha512Cng(),
                    passwordExpirationTime: new Time(DateTime.Now));
                Create(
                    0,
                    "Anonymouse",
                    "Anonymouse",
                    disabled: true);
            }
        }

        private static void Create(
            int tenantId,
            string loginId,
            string name,
            bool disabled = false,
            string password = "",
            Time passwordExpirationTime = null)
        {
            Rds.ExecuteNonQuery(statements:
                Rds.InsertUsers(
                    param: Rds.UsersParam()
                        .TenantId(tenantId)
                        .LoginId(loginId)
                        .Disabled(disabled)
                        .Password(password)
                        .LastName(string.Empty)
                        .FirstName(name)
                        .DeptId(0)
                        .FirstAndLastNameOrder(1)
                        .PasswordExpirationTime(
                            passwordExpirationTime?.ToString(),
                            _using: passwordExpirationTime != null)));
        }
    }
}