namespace EMS.Domain.Enums
{
    public static class PermissionKeys
    {
        public const string UsersView = "users.view";
        public const string UsersCreate = "users.create";
        public const string UsersEdit = "users.edit";
        public const string UsersDelete = "users.delete";

        public const string OrganisationView = "organisation.view";
        public const string OrganisationEdit = "organisation.edit";
        public const string OrganisationDelete = "organisation.delete";

        public const string JoinRequestsView = "joinRequests.view";
        public const string JoinRequestsApprove = "joinRequests.approve";

        public const string TasksView = "tasks.view";
        public const string TasksCreate = "tasks.create";
        public const string TasksEdit = "tasks.edit";
        public const string TasksDelete = "tasks.delete";

        public const string PermissionsView = "permissions.view";
        public const string PermissionsEdit = "permissions.edit";

        public const string MailBoxesView = "mailBoxes.view";
        public const string MailBoxesCreate = "mailBoxes.create";
        public const string MailBoxesEdit = "mailBoxes.edit";
        public const string MailBoxesDelete = "mailBoxes.delete";

        public static readonly IReadOnlyList<string> All =
        [
            UsersView,
            UsersCreate,
            UsersEdit,
            UsersDelete,

            OrganisationView,
            OrganisationEdit,
            OrganisationDelete,

            MailBoxesCreate,
            MailBoxesEdit,
            MailBoxesDelete,
            MailBoxesView,

            TasksCreate,
            TasksEdit,
            TasksDelete,
            TasksView,

            JoinRequestsView,
            JoinRequestsApprove,


            PermissionsView,
            PermissionsEdit
        ];
    }
}
