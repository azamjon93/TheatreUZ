using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public interface IRepository
    {
        #region State

        State GetState(Guid id);

        IEnumerable<State> GetAllStates();

        void SaveState(State obj);

        void EditState(State obj);

        void DeleteState(Guid id);

        #endregion

        #region Role

        Role GetRole(Guid id);

        IEnumerable<Role> GetAllRoles();

        void SaveRole(Role obj);

        void EditRole(Role obj);

        void DeleteRole(Guid id);

        #endregion

        #region User

        User GetUser(Guid id);

        User GetUserByEmail(string email);

        IEnumerable<User> GetAllUsers();

        CommandResponse SaveUser(User obj);

        void EditUser(User obj);

        void DeleteUser(Guid id);

        #endregion

        #region Genre

        Genre GetGenre(Guid id);

        IEnumerable<Genre> GetAllGenres();

        void SaveGenre(Genre obj);

        void EditGenre(Genre obj);

        void DeleteGenre(Guid id);

        #endregion

        #region Spectacle

        Spectacle GetSpectacle(Guid id);

        IEnumerable<Spectacle> GetAllSpectacles();

        void SaveSpectacle(Spectacle obj);

        void EditSpectacle(Spectacle obj);

        void DeleteSpectacle(Guid id);

        #endregion

        #region Sale

        Sale GetSale(Guid id);

        IEnumerable<Sale> GetAllSales();

        void SaveSale(Sale obj);

        void EditSale(Sale obj);

        void DeleteSale(Guid id);

        #endregion

        #region Notification

        Notification GetNotification(Guid id);

        IEnumerable<Notification> GetAllNotifications();

        void SaveNotification(Notification obj);

        void EditNotification(Notification obj);

        void DeleteNotification(Guid id);

        #endregion



    }
}
