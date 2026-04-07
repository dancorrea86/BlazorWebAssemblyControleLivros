# Blazor WebAssembly - Controle de Livros 📚

Sistema moderno de gerenciamento de leitura desenvolvido com **Blazor WebAssembly** e **.NET 10**, utilizando uma arquitetura de API desacoplada e integração com **Google Sheets** como banco de dados persistente.

![Alt text](https://github.com/dancorrea86/BlazorWebAssemblyControleFinancas/blob/master/images/Dashboard.png "Dashboard")

![Alt text](https://github.com/dancorrea86/BlazorWebAssemblyControleFinancas/blob/master/images/Livros.png "Livros")


## 🎯 Objetivos do Projeto

Este projeto foi desenvolvido com os seguintes objetivos de estudo e prática:
- **Blazor WebAssembly:** Exploração do framework para criação de SPAs modernas e performáticas.
- **Servidores Linux:** Prática na configuração e deploy de aplicações .NET em ambiente Linux.
- **Contêineres Docker:** Utilização de Docker para isolamento, portabilidade e padronização dos ambientes de desenvolvimento e produção.
- **Escalabilidade:** Entendimento de como estruturar aplicações desacopladas prontas para orquestração.

## 🚀 Tecnologias Utilizadas

- **Frontend:** Blazor WebAssembly (.NET 10)
- **Backend:** ASP.NET Core Web API (.NET 10)
- **Persistência:** Google Sheets (via Google Apps Script)
- **Infraestrutura:** Docker, Servidor Linux
- **Estilização:** CSS Isolado e Bootstrap
- **Comunicação:** HttpClient Factory e JSON Serialization

## 🏗️ Arquitetura do Projeto

O projeto segue uma estrutura de camadas para garantir separação de responsabilidades:

- **BlazorWebAssemblyControleLivros.Web:** Cliente SPA que roda inteiramente no navegador do usuário. Contém as páginas de Dashboard, Cadastro, Edição e Listagem.
- **BlazorWebAssemblyControleLivros.Api:** Atua como um serviço de backend (Proxy) que gerencia a lógica de negócio e a comunicação segura com a API do Google Sheets.
- **Integração Externa:** Os dados são armazenados em uma planilha do Google, permitindo visualização externa e persistência gratuita sem a necessidade de um servidor de banco de dados tradicional (SQL/NoSQL).

## 📋 Funcionalidades

- [x] **Dashboard:** Visão geral das estatísticas de leitura.
- [x] **Cadastro de Livros:** Registro completo com título, autor, gênero, data de leitura e comentários.
- [x] **Avaliação:** Sistema de notas para os livros lidos.
- [x] **Listagem:** Visualização organizada de todos os livros registrados.
- [x] **Edição/Exclusão:** Manutenção dos registros existentes.
- [x] **Página de Erro 404:** Tratamento de rotas inexistentes de forma amigável.

## 💻 Como Executar

### Pré-requisitos
- .NET 10 SDK (ou superior)
- Docker Desktop (opcional para rodar via contêiner)
- Visual Studio 2022 ou VS Code

### Passo a Passo Local

1. **Clonar o repositório:**
   ```bash
   git clone https://github.com/seu-usuario/BlazorWebAssemblyControleLivros.git
   cd BlazorWebAssemblyControleLivros
   ```

2. **Executar a API:**
   ```bash
   cd BlazorWebAssemblyControleLivros.Api
   dotnet run
   ```

3. **Executar o Frontend:**
   ```bash
   cd ../BlazorWebAssemblyControleLivros.Web
   dotnet run
   ```

### 🐳 Publicação com Docker

Para facilitar a publicação em servidores Linux e garantir que o ambiente seja idêntico ao de desenvolvimento, você pode utilizar o Docker.

#### Criando a Imagem da API:
```bash
# Navegue até a pasta da API
cd BlazorWebAssemblyControleLivros.Api

# Build da imagem
docker build -t controle-livros-api .

# Rodar o contêiner
docker run -d -p 8080:8080 --name api-controle-livros controle-livros-api
```

#### Publicação no Linux:
1. Garanta que o Docker esteja instalado no seu servidor Linux.
2. Faça o push da imagem para um registro (Docker Hub, por exemplo) ou utilize Git para buildar diretamente no servidor.
3. Configure um Proxy Reverso (como Nginx) para gerenciar o tráfego HTTPS e encaminhar para os contêineres.

## 🛠️ Estrutura de Pastas

```text
├── BlazorWebAssemblyControleLivros.Api  # Backend ASP.NET Core
│   ├── Controllers                      # Endpoints da API
│   └── Models                           # DTOs e Modelos de Dados
├── BlazorWebAssemblyControleLivros.Web  # Frontend Blazor WASM
│   ├── Pages                            # Páginas Razor (Home, Cadastro, etc)
│   ├── Layout                           # Componentes de Layout (NavMenu, MainLayout)
│   └── wwwroot                          # Arquivos estáticos (CSS, Imagens)
└── BlazorWebAssemblyControleLivros.slnx # Solução do projeto
```

## 📝 Modelo de Dados (Livro)

O objeto principal do sistema contém:
- `Id`: Identificador único (GUID)
- `Titulo`: Nome da obra
- `Autor`: Escritor(a)
- `Genero`: Categoria literária
- `DataLeitura`: Data em que a leitura foi concluída
- `Avaliacao`: Nota de 1 a 5
- `Comentarios`: Observações pessoais sobre o livro

---
Desenvolvido como projeto de estudos para exploração das novas funcionalidades do .NET 10 e Blazor.
