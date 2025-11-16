

# ZenLog: Ferramenta de Monitoramento de Bem-Estar

## Nome Integrantes

<div align="center">

| Nome | RM |  
| ------------- |:-------------:|  
| Arthur Eduardo Luna Pulini|554848|  
|Lucas Almeida Fernandes de Moraes| 557569 |  
|Victor Nascimento Cosme|558856|

</div>

## Instalação do projeto - Orientações

#### Credenciais
Para rodar o projeto, é necessário inserir as credenciais do banco de dados Oracle da FIAP no arquivo `appsettings.Development.json`.
#### Aplicação das entidades em tabelas no banco de dados
Após inserir as credenciais, deve-se abrir o Packet Manager Console (Tools > NuGet Package Manager > Package Manager Console) e inserir o comando: `update-database` de modo que as entidades sejam refletidas em banco de dados.
#### Rodar o projeto
Feito isso, basta inicializar o projeto via **HTTP** (não HTTPS) e o Swagger da API será aberto automaticamente. Caso isso não ocorra, ele pode ser acessado através da URL `http://localhost:5045/swagger/index.html`.
## Como rodar os testes?
Os testes foram construídos de acordo com as orientações em aula, ou seja, com os devidos mocks. Temos 2 testes na nossa aplicação, o **MotoRepositoryTest** e o **EnderecoRepositoryTest**. 
- Para rodá-los, basta ir ao canto superior esquerdo do Visual Studio e clicar em **"Tests"**. 
- Após isso, clique em **"Test Explorer"** e no canto superior esquerdo da aba que abrir, clique na seta para baixo ao lado do botão de *play*. 
- Clique em "Run All Tests In View".
Feito isso, todos os testes implementados rodarão.

## Versionamento da API
- O versionamento de API foi utilizado no **ColaboradorController**, pois inicialmente (na v1) pensamos que uma busca de colaborador por ID seria interessante. Entretanto, afim de não expor os IDs dos registros dessa entidade no nosso banco de dados, optamos por remover este método do ColaboradorController.
- Isto pode ser visualizado ao selecionar a v2 no canto superior direito do Swagger.

## 🎯 O Projeto

*ZenLog* é uma plataforma web projetada para atuar como um "Log de Emoções" focado no ambiente de trabalho. A ferramenta permite que os colaboradores registrem diariamente suas percepções emocionais e hábitos de vida, fornecendo às empresas um panorama claro sobre o bem-estar de suas equipes.

Diferente de um chatbot, o ZenLog funciona como um diário digital estruturado, onde os dados são inputados ativamente pelo usuário em uma página dedicada ao final do dia.

## 👥 Público-Alvo

Nosso público-alvo são *empresas e equipes de Gestão de Pessoas (RH)* que desejam ter uma noção proativa de como anda a saúde emocional e o bem-estar geral de seus colaboradores, permitindo a criação de estratégias de apoio mais eficazes.

## 🚀 Principais Funcionalidades

O sistema foi desenhado para ser simples e eficaz, focado na coleta de dados relevantes:

### 1. Log Diário de Emoções
Ao final do dia de trabalho, o usuário pode acessar a plataforma para registrar sobre suas emoções e o sentimento geral daquele dia.

### 2. Coleta de Contexto (Evidências)
Para entender as possíveis causas por trás das emoções relatadas, o ZenLog coleta evidências sobre hábitos diários cruciais para o bem-estar:

* *Hidratação:* Quanto de água o usuário tomou no dia?
* *Atividade Física:* O usuário se exercitou?
* *Sono:* Quantas horas o usuário dormiu na noite anterior?

### 4. Insights Semanais - IA
A ideia é que os usuários possam criar correlações entre dados como horas de sono e hidratação com suas emoções no momento. Esses insights são extremamente valiosos se aliados a um acompanhamento psicológico efetivo, pois ajuda o colaborador a identificar gatilhos emocionais.

## Endpoints da API

A URL base para todos os endpoints para testes é: `http://localhost:5152/api/v1/`
### **Health**

-   `GET /api/v1/Health/live` → Verifica se a API está online (Liveness).
    
-   `GET /api/v1/Health/ready` → Verifica se a API e suas dependências estão prontas (Readiness).

### **Empresa**

-   `GET /api/v1/Empresa/{id}` → Busca empresa por ID.
    
-   `GET /api/v1/Empresa` → Lista todas as empresas (paginado).
    
-   `POST /api/v1/Empresa` → Cadastra nova empresa.
    
-   `PUT /api/v1/Empresa/{id}` → Atualiza empresa existente.
    
-   `DELETE /api/v1/Empresa/{id}` → Remove empresa.

### **Colaborador**

-   `GET /api/v1/Colaborador/{id}` → Busca colaborador por ID.
    
-   `GET /api/v1/Colaborador/por-email` → Busca colaborador por Email.
    
-   `GET /api/v1/Colaborador/por-cpf` → Busca colaborador por CPF.
    
-   `GET /api/v1/Colaborador/por-matricula` → Busca colaborador por Matrícula.
    
-   `GET /api/v1/Colaborador` → Lista todos os colaboradores (paginado).
    
-   `GET /api/v1/Colaborador/por-empresa` → Lista colaboradores por empresa (paginado).
    
-   `POST /api/v1/Colaborador` → Cadastra novo colaborador.
    
-   `PUT /api/v1/Colaborador/{id}` → Atualiza colaborador existente.
    
-   `DELETE /api/v1/Colaborador/{id}` → Remove colaborador.
- 
### **LogEmocional**

-   `GET /api/v1/LogEmocional/{id}` → Busca log emocional por ID.
    
-   `GET /api/v1/LogEmocional` → Lista logs por colaborador (paginado, requer `colaboradorId`).
    
-   `POST /api/v1/LogEmocional` → Cadastra novo log emocional.
    
-   `PUT /api/v1/LogEmocional/{id}` → Atualiza log emocional existente.
    
-   `DELETE /api/v1/LogEmocional/{id}` → Remove log emocional.

### **AI (Inteligência Artificial)**

-   `GET /api/v1/AI` → Treina o modelo de ML com os dados existentes no banco.
    
-   `POST /api/v1/AI` → Realiza a predição do nível emocional com base nos inputs.
---

## Roteiro de testes
Aqui estão disponibilizados os JSONs para teste da API. A ordem de execução recomendada é: **Empresa** → **Colaborador** → **LogEmocional**.

### Empresa

**POST** **URL:** `http://localhost:5152/api/v1/Empresa`
```
{
  "id": 0,
  "razaoSocial": "Nova Empresa Tech SA",
  "setor": 1
}
```
---
**POST - CRIADO APENAS PARA DELEÇÃO**
```
{
  "id": 0,
  "razaoSocial": "Empresa de Varejo XYZ",
  "setor": 2
}
```
---
**GET por ID**
**URL:** `http://localhost:5152/api/v1/Empresa/{id}` _(Use o ID retornado na criação da "Nova Empresa Tech SA")_

--- 
**GET (Listar)** **URL:** `http://localhost:5152/api/v1/Empresa?pageNumber=1&pageSize=10`

---
**PUT** 
**URL:** `http://localhost:5152/api/v1/Empresa/{id}`
```
{
  "id": 0,// Use o ID retornado na criação da "Nova Empresa Tech SA"
  "razaoSocial": "Nova Empresa Tech SA (Atualizada)",
  "setor": 1
}
```

---
**DELETE**
**URL:** `http://localhost:5152/api/v1/Empresa/{id}` _(Use o ID retornado na criação da "Empresa de Varejo XYZ")_

---
### Colaborador
**POST** 
**URL:** `http://localhost:5152/api/v1/Colaborador`
```
{
  "id": 0,
  "username": "joaosilva",
  "email": "joaosilva@gmail.com",
  "dataNascimento": "1990-05-20T00:00:00",
  "numeroMatricula": "1234567890",
  "cpf": "12345678901",
  "empresaId": 0 // insira aqui o ID da empresa cadastrada previamente
}
```

---
**POST - CRIADO APENAS PARA DELEÇÃO**
```
{
  "id": 0,
  "username": "mariasouza",
  "email": "mariasouza@gmail.com",
  "dataNascimento": "1995-08-15T00:00:00",
  "numeroMatricula": "0987654321",
  "cpf": "10987654321",
  "empresaId": 0 // insira aqui o ID da empresa cadastrada previamente
}
```

---
**GET por ID** 
**URL:** `http://localhost:5152/api/v1/Colaborador/{id}` _(Use o ID retornado na criação do "joaosilva")_

---
**GET por Email** 
**URL:** `http://localhost:5152/api/v1/Colaborador/por-email?email=joaosilva@gmail.com`

---
**GET por CPF** 
**URL:** `http://localhost:5152/api/v1/Colaborador/por-cpf?cpf=12345678901`

--- 
**PUT** 
**URL:** `http://localhost:5152/api/v1/Colaborador/{id}` _(Use o ID retornado na criação do "joaosilva")_
```
{
  "id": 1,
  "username": "joao.silva.atualizado",
  "email": "joao.silva@novaempresa.com",
  "dataNascimento": "1990-05-20T00:00:00",
  "numeroMatricula": "1234567890",
  "cpf": "12345678901",
  "empresaId": 0 // utilize aqui o ID da empresa criada previamente
}
```
---

**DELETE**
**URL:** `http://localhost:5152/api/v1/Colaborador/{id}` _(Use o ID retornado na criação da "mariasouza")_

---
## LogEmocional
**POST** 
**URL:** `http://localhost:5152/api/v1/LogEmocional`
```
{
  "id": 0,
  "nivelEmocional": 2,
  "descricaoSentimento": "Dia foi produtivo.",
  "fezExercicios": true,
  "horasDescanso": 8,
  "litrosAgua": 2,
  "createdAt": "2025-11-15T14:30:00",
  "colaboradorId": 0 // insira o ID do colaborador criado previamente
}
```
**POST - CRIADO APENAS PARA DELEÇÃO**
```
{
  "id": 0,
  "nivelEmocional": 4,
  "descricaoSentimento": "Muitas reuniões, cansaço.",
  "fezExercicios": false,
  "horasDescanso": 6,
  "litrosAgua": 1,
  "createdAt": "2025-11-14T18:00:00",
  "colaboradorId": 0 // insira o ID do colaborador criado previamente
}
```

---
**GET por ID** 
**URL:** `http://localhost:5152/api/v1/LogEmocional/{id}` _(Use o ID retornado na criação do primeiro log)_

---
**GET por Colaborador** 
**URL:** `http://localhost:5152/api/v1/LogEmocional?colaboradorId=1&pageNumber=1&pageSize=10`

---
**PUT** 
**URL:** `http://localhost:5152/api/v1/LogEmocional/{id}` _(Use o ID retornado na criação do primeiro log)_
```
{
  "id": 1,
  "nivelEmocional": 1,
  "descricaoSentimento": "Dia foi produtivo e terminei minha tarefa.",
  "fezExercicios": true,
  "horasDescanso": 8,
  "litrosAgua": 3,
  "createdAt": "2025-11-15T14:30:00",
  "colaboradorId": 0 // insira o ID do colaborador criado previamente
}
```

---
**DELETE** 
**URL:** `http://localhost:5152/api/v1/LogEmocional/{id}` _(Use o ID retornado na criação do segundo log)_

---
## AI (Inteligência Artificial)
**GET (Treinar Modelo)** _Obs: Execute este endpoint primeiro. Ele usa os logs já cadastrados para treinar o modelo de IA._ 
**URL:** `http://localhost:5152/api/v1/AI`

---
**POST (Predição)** _Obs: Este endpoint simula uma predição. O `nivelEmocional` retornado será o valor previsto pelo modelo._ **URL:** `http://localhost:5152/api/v1/AI`
```
{
  "fezExercicios": 1,
  "horasDescanso": 5,
  "litrosAgua": 1,
  "nivelEmocional": 0
}
``
