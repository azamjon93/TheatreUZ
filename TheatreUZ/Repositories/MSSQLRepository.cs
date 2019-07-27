using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class MSSQLRepository : IRepository
    {
        readonly TheatreUZContext db = new TheatreUZContext();

        #region State

        public State GetState(Guid id)
        {
            return StateQueryHandlerFactory.Build(new OneStateQuery(id), db).Get();
        }

        public IEnumerable<State> GetAllStates()
        {
            var x = StateQueryHandlerFactory.Build(new AllStatesQuery(), db);
            return x.Get();
        }

        public void SaveState(State obj)
        {
            var x = StateSaveCommandHandlerFactory.Build(new StateSaveCommand(obj), db);
            x.Execute();
        }

        public void EditState(State obj)
        {
            var x = StateSaveCommandHandlerFactory.Build(new StateSaveCommand(obj), db);
            x.Execute();
        }

        public void DeleteState(Guid id)
        {
            var x = StateDeleteCommandHandlerFactory.Build(new StateDeleteCommand(id), db);
            x.Execute();
        }

        #endregion

        #region Role

        public Role GetRole(Guid id)
        {
            return RoleQueryHandlerFactory.Build(new OneRoleQuery(id), db).Get();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var x = RoleQueryHandlerFactory.Build(new AllRolesQuery(), db);
            return x.Get();
        }

        public void SaveRole(Role obj)
        {
            var x = RoleSaveCommandHandlerFactory.Build(new RoleSaveCommand(obj), db);
            x.Execute();
        }

        public void EditRole(Role obj)
        {
            var x = RoleSaveCommandHandlerFactory.Build(new RoleSaveCommand(obj), db);
            x.Execute();
        }

        public void DeleteRole(Guid id)
        {
            var x = RoleDeleteCommandHandlerFactory.Build(new RoleDeleteCommand(id), db);
            x.Execute();
        }

        #endregion

        #region User

        public User GetUser(Guid id)
        {
            return UserQueryHandlerFactory.Build(new OneUserQuery(id), db).Get();
        }

        public User GetUserByEmail(string email)
        {
            return UserQueryHandlerFactory.Build(new OneUserByEmailQuery(email), db).Get();
        }

        public IEnumerable<User> GetAllUsers()
        {
            var x = UserQueryHandlerFactory.Build(new AllUsersQuery(), db);
            return x.Get();
        }

        public CommandResponse SaveUser(User obj)
        {
            var x = UserSaveCommandHandlerFactory.Build(new UserSaveCommand(obj), db);
            return x.Execute();
        }

        public void EditUser(User obj)
        {
            var x = UserSaveCommandHandlerFactory.Build(new UserSaveCommand(obj), db);
            x.Execute();
        }

        public void DeleteUser(Guid id)
        {
            var x = UserDeleteCommandHandlerFactory.Build(new UserDeleteCommand(id), db);
            x.Execute();
        }

        #endregion

        #region Genre

        public Genre GetGenre(Guid id)
        {
            return GenreQueryHandlerFactory.Build(new OneGenreQuery(id), db).Get();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var x = GenreQueryHandlerFactory.Build(new AllGenresQuery(), db);
            return x.Get();
        }

        public void SaveGenre(Genre obj)
        {
            var x = GenreSaveCommandHandlerFactory.Build(new GenreSaveCommand(obj), db);
            x.Execute();
        }

        public void EditGenre(Genre obj)
        {
            var x = GenreSaveCommandHandlerFactory.Build(new GenreSaveCommand(obj), db);
            x.Execute();
        }

        public void DeleteGenre(Guid id)
        {
            var x = GenreDeleteCommandHandlerFactory.Build(new GenreDeleteCommand(id), db);
            x.Execute();
        }

        #endregion

        #region Spectacle

        public Spectacle GetSpectacle(Guid id)
        {
            return SpectacleQueryHandlerFactory.Build(new OneSpectacleQuery(id), db).Get();
        }

        public IEnumerable<Spectacle> GetAllSpectacles()
        {
            var x = SpectacleQueryHandlerFactory.Build(new AllSpectaclesQuery(), db);
            return x.Get();
        }

        public void SaveSpectacle(Spectacle obj)
        {
            var x = SpectacleSaveCommandHandlerFactory.Build(new SpectacleSaveCommand(obj), db);
            x.Execute();
        }

        public void EditSpectacle(Spectacle obj)
        {
            var x = SpectacleSaveCommandHandlerFactory.Build(new SpectacleSaveCommand(obj), db);
            x.Execute();
        }

        public void DeleteSpectacle(Guid id)
        {
            var x = SpectacleDeleteCommandHandlerFactory.Build(new SpectacleDeleteCommand(id), db);
            x.Execute();
        }

        #endregion

        #region Sale

        public Sale GetSale(Guid id)
        {
            return SaleQueryHandlerFactory.Build(new OneSaleQuery(id), db).Get();
        }

        public IEnumerable<Sale> GetAllSales()
        {
            var x = SaleQueryHandlerFactory.Build(new AllSalesQuery(), db);
            return x.Get();
        }

        public void SaveSale(Sale obj)
        {
            var x = SaleSaveCommandHandlerFactory.Build(new SaleSaveCommand(obj), db);
            x.Execute();
        }

        public void EditSale(Sale obj)
        {
            var x = SaleSaveCommandHandlerFactory.Build(new SaleSaveCommand(obj), db);
            x.Execute();
        }

        public void DeleteSale(Guid id)
        {
            var x = SaleDeleteCommandHandlerFactory.Build(new SaleDeleteCommand(id), db);
            x.Execute();
        }

        #endregion

        #region Notification

        public Notification GetNotification(Guid id)
        {
            return NotificationQueryHandlerFactory.Build(new OneNotificationQuery(id), db).Get();
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            var x = NotificationQueryHandlerFactory.Build(new AllNotificationsQuery(), db);
            return x.Get();
        }

        public void SaveNotification(Notification obj)
        {
            var x = NotificationSaveCommandHandlerFactory.Build(new NotificationSaveCommand(obj), db);
            x.Execute();
        }

        public void EditNotification(Notification obj)
        {
            var x = NotificationSaveCommandHandlerFactory.Build(new NotificationSaveCommand(obj), db);
            x.Execute();
        }

        public void DeleteNotification(Guid id)
        {
            var x = NotificationDeleteCommandHandlerFactory.Build(new NotificationDeleteCommand(id), db);
            x.Execute();
        }

        #endregion
    }
}