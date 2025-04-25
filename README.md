# APICaixa

API para gerenciamento de contas bancárias fictícias, desenvolvida com .NET 8, utilizando o padrão Clean Architecture e persistência em PostgreSQL.

## 🧰 Tecnologias Utilizadas

- ASP.NET Core 8
- PostgreSQL
- Entity Framework Core
- Docker & Docker Compose
- xUnit + Moq (para testes unitários)

---

## 🚀 Como Rodar o Projeto

### 🔧 Requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/install/)

### 🔁 Rodar com Docker (recomendado)

```bash
docker compose up --build
```
A aplicação estará disponível em:
http://localhost:8080/swagger
⚠️ A primeira execução pode demorar um pouco para baixar as imagens e configurar o banco de dados.

## ⚙️ Rodar localmente (sem Docker)

### Ajuste a connection string no arquivo APICaixa/appsettings.json:
```bash
"ConnectionStrings": {
  "BDCaixa": "Host=localhost;Port=5432;Database=BDCaixa;Username=postgres;Password=APICX2504"
}
```
Execute as migrations (opcional, caso ainda não existam):
```bash
dotnet ef database update --project APICaixa
```
Rode o projeto com:
```bash
dotnet run --project APICaixa
```



## 🗃️ Banco de Dados

- O banco utilizado é PostgreSQL
- As Migrations já estão incluídas no projeto e são aplicadas automaticamente na execução da API
- Estrutura de tabelas: `Contas`, `LogsTransferencia`, `LogsDesativacaoConta`

---

## ✅ Rodando os testes automatizados

O projeto de testes está localizado em:

```
APICaixa.Testes
```

Para rodar os testes, no terminal:

```bash
cd APICaixa.Testes
dotnet test
```

Utilizamos:
- **xUnit** como framework de testes
- **Moq** para mocks e testes unitários

---

## 📦 Estrutura da Solução

```
/APICaixa
│
├── APICaixa/              → Projeto principal da API
├── APICaixa.Testes/       → Projeto com os testes automatizados
├── docker-compose.yml     → Composição dos containers
├── Dockerfile             → Container da aplicação
└── README.md              → Este arquivo
```

---
## 📌 Funcionalidades Implementadas

Cadastro de contas bancárias com saldo inicial de R$1000
Consulta de contas com filtros por nome e documento
Inativação de contas com registro de log
Transferência entre contas ativas com saldo disponível
Documentação da API via Swagger


---
## 📄 Licença

Este projeto é de uso acadêmico e demonstrativo.


---
## 👨💻 Autor
Juliano
Email: julianops79@gmail.com


