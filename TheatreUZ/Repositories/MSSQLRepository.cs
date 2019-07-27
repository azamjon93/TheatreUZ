using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class MSSQLRepository : IRepository
    {
        TheatreUZContext db = new TheatreUZContext();

        #region State

        public State GetState(Guid id)
        {
            return StateQueryHandlerFactory.Build(new OneStateQuery(id)).Get();
        }

        public IEnumerable<State> GetAllStates()
        {
            var x = new AllStatesQueryHandler();
            return x.Get();
        }

        public void SaveState(State obj)
        {
            var x = StateSaveCommandHandlerFactory.Build(new StateSaveCommand(obj));
            x.Execute();
        }

        public void EditState(State obj)
        {
            var x = StateSaveCommandHandlerFactory.Build(new StateSaveCommand(obj));
            x.Execute();
        }

        public void DeleteState(Guid id)
        {
            var x = StateDeleteCommandHandlerFactory.Build(new StateDeleteCommand(id));
            x.Execute();
        }

        #endregion

        #region Role

        public Role GetRole(Guid id)
        {
            return RoleQueryHandlerFactory.Build(new OneRoleQuery(id)).Get();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var x = new AllRolesQueryHandler();
            return x.Get();
        }

        public void SaveRole(Role obj)
        {
            var x = RoleSaveCommandHandlerFactory.Build(new RoleSaveCommand(obj));
            x.Execute();
        }

        public void EditRole(Role obj)
        {
            var x = RoleSaveCommandHandlerFactory.Build(new RoleSaveCommand(obj));
            x.Execute();
        }

        public void DeleteRole(Guid id)
        {
            var x = RoleDeleteCommandHandlerFactory.Build(new RoleDeleteCommand(id));
            x.Execute();
        }

        #endregion

        #region User

        public User GetUser(Guid id)
        {
            return UserQueryHandlerFactory.Build(new OneUserQuery(id)).Get();
        }

        public User GetUserByEmail(string email)
        {
            return UserQueryHandlerFactory.Build(new OneUserByEmailQuery(email)).Get();
        }

        public IEnumerable<User> GetAllUsers()
        {
            var x = new AllUsersQueryHandler();
            return x.Get();
        }

        public CommandResponse SaveUser(User obj)
        {
            var x = UserSaveCommandHandlerFactory.Build(new UserSaveCommand(obj));
            return x.Execute();
        }

        public void EditUser(User obj)
        {
            var x = UserSaveCommandHandlerFactory.Build(new UserSaveCommand(obj));
            x.Execute();
        }

        public void DeleteUser(Guid id)
        {
            var x = UserDeleteCommandHandlerFactory.Build(new UserDeleteCommand(id));
            x.Execute();
        }

        #endregion

        #region Genre

        public Genre GetGenre(Guid id)
        {
            return GenreQueryHandlerFactory.Build(new OneGenreQuery(id)).Get();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var x = new AllGenresQueryHandler();
            return x.Get();
        }

        public void SaveGenre(Genre obj)
        {
            var x = GenreSaveCommandHandlerFactory.Build(new GenreSaveCommand(obj));
            x.Execute();
        }

        public void EditGenre(Genre obj)
        {
            var x = GenreSaveCommandHandlerFactory.Build(new GenreSaveCommand(obj));
            x.Execute();
        }

        public void DeleteGenre(Guid id)
        {
            var x = GenreDeleteCommandHandlerFactory.Build(new GenreDeleteCommand(id));
            x.Execute();
        }

        #endregion

        #region Spectacle

        public Spectacle GetSpectacle(Guid id)
        {
            return SpectacleQueryHandlerFactory.Build(new OneSpectacleQuery(id)).Get();
        }

        public IEnumerable<Spectacle> GetAllSpectacles()
        {
            var x = new AllSpectaclesQueryHandler();
            return x.Get();
        }

        public void SaveSpectacle(Spectacle obj)
        {
            var x = SpectacleSaveCommandHandlerFactory.Build(new SpectacleSaveCommand(obj));
            x.Execute();
        }

        public void EditSpectacle(Spectacle obj)
        {
            var x = SpectacleSaveCommandHandlerFactory.Build(new SpectacleSaveCommand(obj));
            x.Execute();
        }

        public void DeleteSpectacle(Guid id)
        {
            var x = SpectacleDeleteCommandHandlerFactory.Build(new SpectacleDeleteCommand(id));
            x.Execute();
        }

        #endregion

        #region Sale

        public Sale GetSale(Guid id)
        {
            return SaleQueryHandlerFactory.Build(new OneSaleQuery(id)).Get();
        }

        public IEnumerable<Sale> GetAllSales()
        {
            var x = new AllSalesQueryHandler();
            return x.Get();
        }

        public void SaveSale(Sale obj)
        {
            var x = SaleSaveCommandHandlerFactory.Build(new SaleSaveCommand(obj));
            x.Execute();
        }

        public void EditSale(Sale obj)
        {
            var x = SaleSaveCommandHandlerFactory.Build(new SaleSaveCommand(obj));
            x.Execute();
        }

        public void DeleteSale(Guid id)
        {
            var x = SaleDeleteCommandHandlerFactory.Build(new SaleDeleteCommand(id));
            x.Execute();
        }

        #endregion

        #region Notification

        public Notification GetNotification(Guid id)
        {
            return NotificationQueryHandlerFactory.Build(new OneNotificationQuery(id)).Get();
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            var x = new AllNotificationsQueryHandler();
            return x.Get();
        }

        public void SaveNotification(Notification obj)
        {
            var x = NotificationSaveCommandHandlerFactory.Build(new NotificationSaveCommand(obj));
            x.Execute();
        }

        public void EditNotification(Notification obj)
        {
            var x = NotificationSaveCommandHandlerFactory.Build(new NotificationSaveCommand(obj));
            x.Execute();
        }

        public void DeleteNotification(Guid id)
        {
            var x = NotificationDeleteCommandHandlerFactory.Build(new NotificationDeleteCommand(id));
            x.Execute();
        }

        #endregion

    }
}