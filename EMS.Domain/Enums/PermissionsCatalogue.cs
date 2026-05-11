namespace EMS.Domain.Enums
{
    public static class PermissionCatalog
    {
        public static readonly IReadOnlyList<PermissionDefinition> All =
        [
            new()
            {
                Key = PermissionKeys.UsersView,
                Group = "Users",
                Name = "View users",
                Description = "Allows viewing organisation users."
            },
            new()
            {
                Key = PermissionKeys.UsersCreate,
                Group = "Users",
                Name = "Create users",
                Description = "Allows creating users."
            },
            new()
            {
                Key = PermissionKeys.UsersEdit,
                Group = "Users",
                Name = "Edit users",
                Description = "Allows editing users."
            },
            new()
            {
                Key = PermissionKeys.UsersDelete,
                Group = "Users",
                Name = "Delete users",
                Description = "Allows deleting users."
            },
            new()
            {
                Key = PermissionKeys.OrganisationView,
                Group = "Organisation",
                Name = "View organisation",
                Description = "Allows viewing organisation details."
            },
            new()
            {
                Key = PermissionKeys.OrganisationEdit,
                Group = "Organisation",
                Name = "Edit organisation",
                Description = "Allows editing organisation details."
            },
            new()
            {
                Key = PermissionKeys.OrganisationDelete,
                Group = "Organisation",
                Name = "Delete organisation",
                Description = "Allows deleting organisation details."
            },
            new()
            {
                Key = PermissionKeys.JoinRequestsView,
                Group = "Join Requests",
                Name = "View join requests",
                Description = "Allows viewing join requests."
            },
            new()
            {
                Key = PermissionKeys.JoinRequestsApprove,
                Group = "Join Requests",
                Name = "Approve join requests",
                Description = "Allows approving or rejecting join requests."
            },
            new()
            {
                Key = PermissionKeys.TasksView,
                Group = "Tasks",
                Name = "View tasks",
                Description = "Allows viewing tasks."
            },
            new()
            {
                Key = PermissionKeys.TasksCreate,
                Group = "Tasks",
                Name = "Create tasks",
                Description = "Allows creating tasks."
            },
            new()
            {
                Key = PermissionKeys.TasksEdit,
                Group = "Tasks",
                Name = "Edit tasks",
                Description = "Allows editing tasks."
            },
                        new()
            {
                Key = PermissionKeys.TasksDelete,
                Group = "Tasks",
                Name = "Delete tasks",
                Description = "Allows deleting tasks."
            },
            new()
            {
                Key = PermissionKeys.PermissionsView,
                Group = "Permissions",
                Name = "View permissions",
                Description = "Allows viewing permissions."
            },
            new()
            {
                Key = PermissionKeys.PermissionsEdit,
                Group = "Permissions",
                Name = "Edit permissions",
                Description = "Allows changing the role permission matrix."
            },
            new()
            {
                Key = PermissionKeys.MailBoxesView,
                Group = "MailBoxes",
                Name = "View mailboxes",
                Description = "Allows viewing mailboxes."
            },
            new()
            {
                Key = PermissionKeys.MailBoxesCreate,
                Group = "MailBoxes",
                Name = "Create mailboxes",
                Description = "Allows creating mailboxes."
            },
            new()
            {
                Key = PermissionKeys.MailBoxesDelete,
                Group = "MailBoxes",
                Name = "Delete mailboxes",
                Description = "Allows deleting mailboxes."
            },
            new()
            {
                Key = PermissionKeys.MailBoxesEdit,
                Group = "MailBoxes",
                Name = "Edit mailboxes",
                Description = "Allows editing mailboxes."
            },
        ];

        public static bool IsValid(string key) => All.Any(permission => permission.Key == key);
    }
}
