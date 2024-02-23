# Lista de Tarefas

Uma simples aplicação de Lista de Tarefas desenvolvida em C# e utilizando MySQL como banco de dados.

## Funcionalidades

- Adicionar novas tarefas.
- Marcar tarefas como concluídas.
- Remover tarefas.
- Visualizar todas as tarefas.

## Pré-requisitos

Antes de começar, certifique-se de ter os seguintes requisitos:

- .NET 8.0 instalado.
- MySQL Server instalado e configurado.
- MySQL WorkBench instalado e configurado.

## Instalação

1. Clone o repositório:

```bash
git clone https://github.com/seu_usuario/lista-de-tarefas.git
```
2. Abra o projeto em sua IDE preferida.

3. Configure a conexão com o banco de dados.

4. Crie o banco de dados e a tabela no MySQL WorkBench.
```bash
CREATE SCHEMA `umprojeto` ;
USE umprojeto;
CREATE TABLE lista (
 id_lista INT AUTO_INCREMENT PRIMARY KEY ,
 tarefa VARCHAR(255),
 data_tarefa DATE,
 concluida_tarefa BOOL);
```

6. Compile e execute o projeto.

## Configuração
Para configurar a conexão com o banco de dados, na primeira linha do programa atualize de acordo com as suas configurações.

```bash
{
  string conexao = "Server=localhost;DataBase=banco_de_dados;User Id=usuario;Password=senha";
}
```

## Uso
- Ao iniciar a aplicação, você será apresentado com a lista de tarefas.
- Siga todas as instruções escritas no console.
- Tecle "enter" para passar para as próximas instruções.

## Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir um problema ou enviar um pull request.

## Licença
Este projeto está licenciado sob a MIT License.
