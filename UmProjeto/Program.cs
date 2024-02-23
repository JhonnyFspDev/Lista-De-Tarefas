using MySql.Data.MySqlClient;
using Mysqlx.Prepare;

namespace ListaDeTarefas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Fazendo conexão com o Banco de Dados MySql
            string conexao = "Server=localhost;DataBase=umprojeto;User Id=root;Password=123";
            MySqlConnection conn = new MySqlConnection(conexao);

            try
            {
                //Inicializando a classe tarefas
                Tarefas tarefas = new();
                conn.Open();

                tarefas.AberturaCodigo();

                tarefas.ChatAdicionarTarefa(conn);

                tarefas.ChatCheckTarefa(conn);

                tarefas.ChatRemover(conn);

                tarefas.ChatFinalizarPrograma(conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            finally { conn.Close(); }
            
            Console.ReadLine();
        }
    }
}   
