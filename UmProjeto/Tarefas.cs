using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas
{
    public class Tarefas
    {
        public void AberturaCodigo()
        {
            //Mensagem de abertura do código que aparece no início

            Console.WriteLine("******************************************");
            Console.WriteLine("******** Bem vindo as suas tarefas *******");
            Console.WriteLine("******************************************");

            Console.WriteLine("\nEsta é sua lista de Tarefas\n");
        }

        public void ChatAdicionarTarefa(MySqlConnection conn)
        {
            //Esse Código faz a interação da máquina com o usuário para adicionar uma tarefa e a data de realização


            //Invoca o método para exibir todas as tarefas já listadas
            ExibirTarefas(conn);

            //Perguntas para o usuário e guarda a resposta na variável
            Console.WriteLine("\nVocê quer adicionar alguma Tarefa?");
            Console.WriteLine("S para Sim e N para Não.");
            string adicionar = Console.ReadLine();

            //Coloca a variável em maiúscula para evitar erro e testa os casos
            switch (adicionar.ToUpper())
            {
                //Caso queira adicionar uma tarefa
                case "S":
                    
                    //Aqui será digitado a tarefa e guardado na variável
                    Console.WriteLine("Digite a tarefa que deseja Adicionar");
                    string addTarefa = Console.ReadLine();
                    
                    //Se o usuário não digitar nada ele vai continuar repetindo a mensagem dentro do loop
                    while (addTarefa.Equals(""))
                    {
                        Console.WriteLine("Digite um valor Válido");
                        addTarefa = Console.ReadLine();
                    }

                    //Limpa a tela para ficar mais visualizável
                    Console.Clear();

                    //Exibe novamente as tarefas e pede para inserir a Data de realização da tarefa e guarda na variavel
                    ExibirTarefas(conn);
                    Console.WriteLine("Insira a Data a ser realizada no formato AAAA-MM-DD");
                    string dataDigitada = Console.ReadLine();

                    //Mesmo teste de loop caso o usuário não digite nada
                    while (dataDigitada.Equals(""))
                    {
                        Console.WriteLine("Digite uma data válida");
                        dataDigitada = Console.ReadLine();
                    }

                    //Faz um teste, se caso o método AdicionarData execute, a tarefa é adicionada e a data também
                    //Faz um INSERT das tarefas e um UPDATE da Data
                    //É uma forma de evitar o valor nulo na Data e evitar erro de NullReference na hora de exibir
                    if (AdicionarData(conn, dataDigitada, addTarefa) == true)
                    {
                        AdicionarTarefa(conn, addTarefa);
                        AdicionarData(conn, dataDigitada, addTarefa);

                    }
                    else
                    {
                        Console.WriteLine("Tarefa não Adicionada");
                        Console.ReadLine();
                    }
                    break;

                default:
                    break;
            }
        }

        public void ChatCheckTarefa(MySqlConnection conn)
        {
            //Limpa o console e Exibe as tarefas
            Console.Clear();
            ExibirTarefas(conn);

            //Interação sobre se concluiu alguma tarefa e guarda a resposta na variável
            Console.WriteLine("\nConcluiu alguma tarefa?\n");
            Console.WriteLine("Tecle S para sim e N para não");
            string concluida = Console.ReadLine();

            //Coloca a resposta em maiúscula para evitar erros e testa os casos
            switch (concluida.ToUpper())
            {

                //Testa no caso do usuário querer marcar a tarefa a ser concluida
                case "S":
                    Console.WriteLine("\nDigite o ID da tarefa concluida");
                    string check = Console.ReadLine();

                    //Loop para verificar se foi digitado algo
                    while (check.Equals(""))
                    {
                        Console.WriteLine("Digite um valor válido");
                        check = Console.ReadLine();
                    }
                    //Faz um UPDATE no banco de dados
                    //Possui uma interação de uma [X] no método ExibirTarefas caso seja True
                    CheckTarefa(conn, check, true);
                    break;

                default:
                    break;
            }
        }

        public void ChatRemover(MySqlConnection conn)
        {
            //Limpa o Console e Exibe as tarefas novamente
            Console.Clear();
            ExibirTarefas(conn);

            //Pergunta se quer remover a tarefa e guarda na variável
            Console.WriteLine("\nVocê quer remover alguma tarefa?\n");
            Console.WriteLine("S para Sim e N para Não.");
            string remover = Console.ReadLine();

            //Coloca a resposta em maiúscula para evitar erros e testa os casos
            switch (remover.ToUpper())
            {
                //Testa no caso do usuário querer remover a tarefa
                case "S":
                    //A tarefa será removida através do ID para facilitar a remoção
                    Console.WriteLine("Digite o Id da Tarefa que deseja Remover");
                    string delTarefa = Console.ReadLine();

                    //Loop caso o Usuário nao digite nada
                    while (delTarefa.Equals(""))
                    {
                        Console.WriteLine("Digite um valor Válido");
                        delTarefa = Console.ReadLine();
                    }

                    //Faz um DELETE no banco de dados usando o ID como referencia
                    RemoverTarefa(conn, delTarefa);
                    break;

                default:
                    break;
            }
        }

        public void ChatFinalizarPrograma(MySqlConnection conn)
        {
            //Por fim ele novamente limpa o Console e Exibe as todas as tarefas para que no fim o usuário veja as alterações feitas
            Console.Clear();
            Console.WriteLine("Aqui estão suas tarefas\n");
            ExibirTarefas(conn);
        }

        public void ExibirTarefas(MySqlConnection conn)
        {
            try
            {
                //Criação de um comando a partir da conexão com o BD
                MySqlCommand cmd = conn.CreateCommand();

                //Selecão de toda a tabela
                cmd.CommandText = "SELECT * FROM lista;";

                //Leitura de todas as colunas
                using MySqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Tarefas Exibidas\n");

                //Loop para passar por cada leitura feita pelo SELECT
                while (reader.Read())
                {
                    
                    //Teste caso o a terefa foi concluida, ao invés de exibir "False" ou "True", será exibido [] ou [X] como um CheckBox
                    if (reader.GetBoolean("concluida_tarefa") == false)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"ID:{reader.GetInt32("id_lista")} | Tarefa: {reader.GetString("tarefa")} | Data: {reader.GetDateTime("data_tarefa")} | Concluida: []");
                    }
                    else
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"ID:{reader.GetInt32("id_lista")} | Tarefa: {reader.GetString("tarefa")} | Data: {reader.GetDateTime("data_tarefa")} | Concluida: [X]");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private void AdicionarTarefa(MySqlConnection conn, string tarefa)
        {
            try
            {
                //Criação de um comando a partir da conexão com o BD
                MySqlCommand cmd = conn.CreateCommand();

                //Inserir na tabela a tarefa que o usuário digitou
                //Por padrão a conclusão da tarefa é False, além de ser intuitivo evita o erro de registro Nulo na hora de exibir as tarefas
                cmd.CommandText = "INSERT INTO lista (tarefa, concluida_tarefa) VALUES (@tarefa, false);";

                //Adiciona o valor de acordo com o parâmentro
                cmd.Parameters.AddWithValue(@"tarefa", tarefa);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Tarefa adicionada");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private bool AdicionarData(MySqlConnection conn, string dataDigitada, string tarefa)
        {
            //Formato da data AAAA-MM-DD
            DateOnly data;

            //Verificação se a ddata que o usuário digitou pode ser convertida
            if (DateOnly.TryParse(dataDigitada, out data))
            {
                try
                {
                    //Criação de um comando a partir da conexão com o BD
                    MySqlCommand cmd = conn.CreateCommand();

                    //Atualiza o registro da Data usando o tarefa como referência
                    //O método AdicionarData e o AdicionarTarefa andam juntos
                    cmd.CommandText = "UPDATE lista SET data_tarefa = (@data) WHERE tarefa = (@tarefa);";

                    //A data que o usuário escreveu vem para o formato que o Banco de Dados aceita
                    string dataFormatada = data.ToString("yyyy-MM-dd");

                    //Adiciona os valores de acordo com os parâmentros
                    cmd.Parameters.AddWithValue(@"data", dataFormatada);
                    cmd.Parameters.AddWithValue(@"tarefa", tarefa);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Data inserida com Sucesso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro; {ex.Message}");
                }
                //Caso seja executado retorna true
                return true;
            }
            else
            {
                //Caso não exibe essa mensagem de erro e retorna falso
                Console.WriteLine("Formato da Data errado");
                return false;
            }
        }

        private void CheckTarefa(MySqlConnection conn, string id, bool concluida)
        {
            try
            {
                //Criação de um comando a partir da conexão com o BD
                MySqlCommand cmd = conn.CreateCommand();

                //Atualização da tabela caso a tarefa seja concluida
                cmd.CommandText = "UPDATE lista SET  concluida_tarefa = (@concluida) WHERE id_lista = (@id);";

                //Adiciona os valores de acordo com os parâmentros
                cmd.Parameters.AddWithValue(@"concluida", concluida);
                cmd.Parameters.AddWithValue(@"id", id);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private void RemoverTarefa(MySqlConnection conn, string id)
        {
            try
            {
                //Criação de um comando a partir da conexão com o BD
                MySqlCommand cmd = conn.CreateCommand();

                //Aplica um DELETE pelo ID que o usuário digitar
                cmd.CommandText = "DELETE FROM lista WHERE id_lista = (@id);";

                //Adiciona o valor de acordo com o parâmentro
                cmd.Parameters.AddWithValue(@"id", id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Tarefa Removida");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }
    }
}
