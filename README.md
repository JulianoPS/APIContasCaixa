# APICaixa

API para gerenciamento de contas bancárias fictícias, construída com .NET 8, arquitetura Clean Architecture e persistência em PostgreSQL.

## 🧰 Tecnologias Utilizadas

- ASP.NET Core 8
- PostgreSQL
- Entity Framework Core
- Docker e Docker Compose
- xUnit e Moq (para testes)

---

## 🚀 Como rodar o projeto localmente

### 2. Ajuste da Connection String (opcional)

Se for rodar fora do Docker, abra o arquivo:

```bash
APICaixa/appsettings.json
```

E ajuste a `ConnectionStrings:BDCaixa` para refletir os dados do seu PostgreSQL local, exemplo:

```json
"ConnectionStrings": {
  "BDCaixa": "Host=localhost;Port=5432;Database=BDCaixa;Username=postgres;Password=APICX2504"
}
```

### 3. Executar com Docker (recomendado)

Certifique-se de ter o Docker e o Docker Compose instalados.

Execute os comandos:

```bash
docker-compose up --build
```

A API será acessível via:

```
http://localhost:8080/swagger
```

> ⚠️ A primeira vez pode demorar um pouco, pois o Docker irá baixar as imagens e criar o banco de dados.

---

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

## 📄 Licença

Este projeto é de uso acadêmico e demonstrativo.



