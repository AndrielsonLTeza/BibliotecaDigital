# API de Biblioteca Digital

Este projeto implementa uma API RESTful em C# usando ASP.NET Core com arquitetura em camadas e integração com Docker. O sistema gerencia o relacionamento entre Livros e Gêneros, com persistência em PostgreSQL.

## Estrutura do Projeto

O projeto está organizado em três camadas principais:

- **BibliotecaDigital.Core**: Entidades de domínio, interfaces de serviço e regras de negócio
- **BibliotecaDigital.Infrastructure**: Implementação do DbContext, repositórios e persistência
- **BibliotecaDigital.Api**: Controladores REST, configuração de DI e endpoints

## Funcionalidades
-Cadastro de livros com título, autor, ISBN, ano de publicação e gênero.
-Cadastro de gêneros literários.
-Relacionamento entre livros e seus respectivos gêneros.
-API RESTful com suporte a operações CRUD.
-Integração com Swagger para documentação e testes da API.

## Modelo de Dados

### Entidade Gênero
- Id (int, chave primária)
- Name (string, obrigatório)
- Description (string, opcional)

### Entidade Livro
- Id (int, chave primária)
- Title (string, obrigatório)
- Author (string, obrigatório)
- ISBN (string, obrigatório)
- PublishedYear (int, obrigatório)
- GenreId (int, chave estrangeira)

## Tecnologias Utilizadas
ASP.NET Core 8.0
Entity Framework Core
PostgreSQL (ou outro banco relacional)
Swagger
AutoMapper

## Pré-requisitos

- [Docker](https://www.docker.com/products/docker-desktop/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- .NET SDK 8.0
-PostgreSQL (ou outro banco configurado no appsettings.json)
-Visual Studio 2022 ou Visual Studio Code
-Ferramenta de gerenciamento de pacotes (NuGet)

## Como Executar o Projeto

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/biblioteca-digital.git
cd biblioteca-digital
```
### 1.1. Compilação do projeto   

```bash
dotnet build
```

### 2. Configure variáveis de ambiente (opcional)

Crie um arquivo `.env` na raiz do projeto:

```
DB_USER=postgres
DB_PASSWORD=postgres
DB_NAME=BibliotecaDigital
```
### 3. Criação e execução do Docker Compose

### 3.1. Construção com Docker Compose
```bash
docker-compose up --build
```
### 3.2. Execute com Docker Compose

```bash
docker-compose up -d
```

Este comando irá:
- Criar um container PostgreSQL
- Compilar e criar um container para a API
- Aplicar migrações ao banco de dados
- Expor a API na porta 5000:8080

### 4. Acesse a API

A API estará disponível em: http://localhost:5000

Documentação Swagger: http://localhost:5000/swagger

## Endpoints da API

### Gêneros

#### Listar todos os gêneros
```
GET /genres
```

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "name": "Ficção Científica",
    "description": "Livros que exploram conceitos científicos futuristas"
  },
  {
    "id": 2,
    "name": "Romance",
    "description": "Obras com foco em relacionamentos"
  }
]
```

#### Obter um gênero específico
```
GET /genres/{id}
```

**Exemplo de resposta:**
```json
{
  "id": 1,
  "name": "Ficção Científica",
  "description": "Livros que exploram conceitos científicos futuristas"
}
```

#### Criar um novo gênero
```
POST /genres
```

**Corpo da requisição:**
```json
{
  "name": "Fantasia",
  "description": "Livros com elementos mágicos e sobrenaturais"
}
```

#### Atualizar um gênero
```
PUT /genres/{id}
```

**Corpo da requisição:**
```json
{
  "id": 3,
  "name": "Fantasia Épica",
  "description": "Livros com elementos mágicos em mundos complexos"
}
```

#### Excluir um gênero
```
DELETE /genres/{id}
```

#### Listar livros de um gênero
```
GET /genres/{id}/books
```

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "title": "Duna",
    "author": "Frank Herbert",
    "isbn": "9788576572664",
    "publishedYear": 1965,
    "genreId": 1
  }
]
```

### Livros

#### Listar todos os livros
```
GET /books
```

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "title": "Duna",
    "author": "Frank Herbert",
    "isbn": "9788576572664",
    "publishedYear": 1965,
    "genreId": 1,
    "genre": {
      "id": 1,
      "name": "Ficção Científica",
      "description": "Livros que exploram conceitos científicos futuristas"
    }
  }
]
```

#### Obter um livro específico
```
GET /books/{id}
```

**Exemplo de resposta:**
```json
{
  "id": 1,
  "title": "Duna",
  "author": "Frank Herbert",
  "isbn": "9788576572664",
  "publishedYear": 1965,
  "genreId": 1,
  "genre": {
    "id": 1,
    "name": "Ficção Científica",
    "description": "Livros que exploram conceitos científicos futuristas"
  }
}
```

#### Listar livros por gênero
```
GET /books/by-genre/{genreId}
```

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "title": "Duna",
    "author": "Frank Herbert",
    "isbn": "9788576572664",
    "publishedYear": 1965,
    "genreId": 1,
    "genre": {
      "id": 1,
      "name": "Ficção Científica",
      "description": "Livros que exploram conceitos científicos futuristas"
    }
  }
]
```

#### Criar um novo livro
```
POST /books
```

**Corpo da requisição:**
```json
{
  "title": "Neuromancer",
  "author": "William Gibson",
  "isbn": "9788576572279",
  "publishedYear": 1984,
  "genreId": 1
}
```

#### Atualizar um livro
```
PUT /books/{id}
```

**Corpo da requisição:**
```json
{
  "id": 2,
  "title": "Neuromancer: Edição Especial",
  "author": "William Gibson",
  "isbn": "9788576572279",
  "publishedYear": 1984,
  "genreId": 1
}
```

#### Excluir um livro
```
DELETE /books/{id}
```

## Arquitetura do Projeto

```
BibliotecaDigital/
├── BibliotecaDigital.Core/             # Camada de domínio
│   ├── Entities/                       # Entidades (Book, Genre)
│   │   ├── Book.cs
│   │   └── Genre.cs
│   ├── Interfaces/                     # Interfaces de Repositórios
│   │   ├── IBookRepository.cs
│   │   └── IGenreRepository.cs
│   └── Services/                       # Serviços de negócio
│       ├── BookService.cs
│       ├── GenreService.cs
│       ├── IBookService.cs
│       └── IGenreService.cs
├── BibliotecaDigital.Infrastructure/   # Infraestrutura e acesso a dados
│   ├── Data/                           # Contexto do banco de dados (AppDbContext)
│   │   └── ApplicationDbContext.cs
│   ├── Mappings/                       # Configurações do EF Core
│   └── Repositories/                   # Implementações dos repositórios
│       ├── BookRepository.cs
│       └── GenreRepository.cs
├── BibliotecaDigital.Api/              # Projeto principal da API
│   ├── Controllers/                    # Controllers da API (Books, Genres)
│   │   ├── BooksController.cs
│   │   └── GenresController.cs
│   ├──  Program.cs                     # Ponto de entrada da aplicação
│   └── appsettings.json                # Configurações da aplicação
├── .dockerignore                       # Arquivos ignorados no build Docker
├── .gitignore                          # Arquivos ignorados no Git
├── BibliotecaDigital.sln               # Solução do projeto
├── Dockerfile                          # Dockerfile para a API
├── docker-compose.yml                  # Orquestração com PostgreSQL
└── README.md   
```

## Diagrama de Arquitetura

```
┌───────────────────┐     ┌────────────────────────┐     ┌───────────────────┐
│                   │     │                        │     │                   │
│  BibliotecaDigital│     │ BibliotecaDigital      │     │ BibliotecaDigital │
│  .Api             │────►│ .Core                  │◄────│ .Infrastructure   │
│                   │     │                        │     │                   │
└───────────────────┘     └────────────────────────┘     └───────────────────┘
         │                                                        │
         │                                                        │
         │                                                        ▼
         │                                               ┌───────────────────┐
         │                                               │                   │
         └───────────────────────────────────────────────►    PostgreSQL     │
                                                         │                   │
                                                         └───────────────────┘
```

## Resolução de Problemas

### Problema: API não conecta ao banco de dados
Verifique se:
1. O container do PostgreSQL está em execução (`docker ps`)
2. As variáveis de ambiente estão configuradas corretamente
3. A string de conexão está correta em `appsettings.json`

### Problema: Migrações não são aplicadas
Execute manualmente:
```bash
docker-compose exec api dotnet ef database update
```

### Problema: Erros 404 ao acessar endpoints
Verifique:
1. Se está usando as rotas corretas conforme documentação
2. Se a API está em execução (`docker ps`)
3. Se não há erros nos logs (`docker-compose logs api`)

## Alguns comandos que ajudaram 
 1. Execute com Docker Compose

```bash
docker-compose up -d
```
 2. Parar de executar o Docker Compose

```bash
docker-compose down
```
2.1 Parar e apagar o Docker Compose
```bash
docker-compose down -v
```
 3. Conferir se o containers estão online

```bash
docker ps
```

4. Mostrar o log da Api

```bash
docker-compose logs api
```

5. Limpar Compilacão anterior
   
Este comando limpa os arquivos de saída gerados por uma compilação anterior. 
```bash
dotnet clean
```


6. Compilar Projeto
   
Este comando compila um projeto .NET e todas as suas dependências. 
```bash
dotnet build
```
