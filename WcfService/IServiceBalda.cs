using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceBalda
    {

        /// <summary>
        /// Определение синхронизации серверной БД
        /// </summary>
        [OperationContract]
        void ActivateSqlSync();

        /// <summary>
        /// Поолучить ключ для игрока и записать его в БД
        /// </summary>
        /// <returns>ключ игрока</returns>
        [OperationContract]
        Gamer Registration(String IdDevice);

        /// <summary>
        /// начать игру, записав 2 участников в новую таблицу и убрав из таблицы ожиданий
        /// </summary>
        /// <returns>Объект игры</returns>
        [OperationContract]
        Game StartGame();

        /// <summary>
        /// получить случайное слово, длиною 5 букв
        /// </summary>
        /// <returns>слово, длиною 5 букв</returns>
        [OperationContract]
        String GetRandomWord();

        /// <summary>
        /// партия окончена, либо один из участников покинул сессию (второй автоматический тоже ее покидает) 
        /// </summary>
        [OperationContract]
        void EndGame();

        /// <summary>
        /// игрок делает ход, данные записываются в БД, передается ход напарнику
        /// </summary>
        [OperationContract]
        void DoStroke();

        /// <summary>
        /// передача хода напарнику (по истечению времени ожидания хода напарника либо если напарник сделал ход)
        /// </summary>
        [OperationContract]
        void TransmitStroke();

        /// <summary>
        /// получить количество шагов
        /// </summary>
        /// <param name="id">игра</param>
        /// <param name="idGamer">игрок</param>
        /// <returns>кол-во шагов</returns>
        [OperationContract]
        int GetStroke(int id, int idGamer);

        /// <summary>
        /// получить количество очков
        /// </summary>
        /// <param name="id">игра</param>
        /// <param name="idGamer">игрок</param>
        /// <returns>кол-во очков</returns>
        [OperationContract]
        int GetNumbers(int id, int idGamer);
    }
}
