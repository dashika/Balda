using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.Server;
using Microsoft.Synchronization.Data.SqlServer;
using System.IO;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceBalda : IServiceBalda
    {
        int MaxLengthWord = 32;
        int CountOfWordFive = 4553;

       public void ActivateSqlSync()
        {
            SqlConnection serverConn = new SqlConnection("Data Source=localhost; Initial Catalog=Balda; Integrated Security=True");

            // Определить новую область с именем ProductsScope 
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("BaldaScope");

            // Получаем описание таблицы Изделия из SyncDB dtabase 
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Dictionary", serverConn);
            // Добавить описание таблицы для определения синхронизации области видимости
            scopeDesc.Tables.Add(tableDesc);

          //  List<DbSyncTableDescription> tableDescD = new List<DbSyncTableDescription>();

            for (int i = 3; i < MaxLengthWord; i++ )
            {
                scopeDesc.Tables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Dictionary" + i, serverConn));
            }

            // create a server scope provisioning object based on the ProductScope
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);

            // skipping the creation of table since table already exists on server
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);

            // start the provisioning process
            serverProvision.Apply();

        }

        /// <summary>
        /// регистрируем игрока, и даем ему уникальный ключ
        /// </summary>
        /// <returns>данные об игроке</returns>
        public Gamer Registration(String IdDevice)
        {
            using(BaldaEntities BE = new BaldaEntities())
            {
                ListOfGamer player = new ListOfGamer();
                try
                {
                    // ищем игрока
                   var gamer = (from c in BE.Gamers
                             where c.UnicId.Contains(IdDevice)
                             select c).SingleOrDefault();
                   // добавляем игрока в список ожидающих напарника
                   player.IdGamer = gamer.Id;
                   BE.ListOfGamers.Add(player);
                   BE.SaveChanges();

                   return gamer;
                }
                catch (Exception ex)
                {
                    // если такой игрок еще не зарегистрирован - создаем
                    Gamer gamer = new Gamer();
                        gamer.UnicId = IdDevice;
                        gamer.Name = Guid.NewGuid().ToString();
                        BE.Gamers.Add(gamer);
                        BE.SaveChanges();
                        // добавляем игрока в список ожидающих напарника
                        player.IdGamer = gamer.Id;
                        BE.ListOfGamers.Add(player);
                        BE.SaveChanges();

                        return gamer;              
                }              
            }
        }


        /// <summary>
        /// анализ колическа ожидаемых пользователей и в случае кратности - начало игры
        /// </summary>
        /// <returns>объект игры либо пустой объект, если парнеров не хватает </returns>
        public Game StartGame()
        {
            using(var db = new BaldaEntities())
            {
                var players = (from c in db.ListOfGamers
                               where c.IdState == null
                               select c).ToList();
                // если в списке более двух ожидающих пользователей, перемещаем их в "игра" и удалем из "списка ожидаемых"
                if(players.Count > 1 )
                {
                    Game game = new Game();
                    game.IdFirstPlayer = players[0].IdGamer;
                    game.IdSecondPlayer = players[1].IdGamer;
                    game.WhoseStep = "0";

                    db.Games.Add(game);
                    db.ListOfGamers.Remove(players[0]);
                    db.ListOfGamers.Remove(players[1]);

                    return game;
                }
                return new Game();
            }
        }

        public String GetRandomWord()
        {
            using (var db = new BaldaEntities())
            {
                ///получить случайное слов из 5 букв
                Random rand = new Random();
                Dictionary5 data = (from c in db.Dictionary5
                                    where c.ID == rand.Next()
                                    select c).SingleOrDefault();

                db.SaveChanges();

                return data.Word;
            }

        }

        public void EndGame()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// сделан ход
        /// </summary>
        public void DoStroke()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// пропуск хода
        /// </summary>
        public void TransmitStroke()
        {
            throw new NotImplementedException();
        }

        public int GetStroke(int id, int idGamer)
        {
            throw new NotImplementedException();
        }

        public int GetNumbers(int id, int idGamer)
        {
            throw new NotImplementedException();
        }
    }
}
