# SGM - Sistema de Gestão Municipal - (BACK-END)
 Solução desenvolvida para atender requisitos da prova de conceito do TCC do curso de Arquitetura de Software Distribuido da PUC Minas.

## Instalação
* Realize o clone da Aplicação.
* Na pasta Raiz do projeto, acione o comando docker-compose up para subir imagem do kafka,zoopeeker e kafdrop.
* Nos arquivos dos SERVICOS/(Cidadao,Manager e Saude)/ appsettings.json, use o seu banco de dados SQL server para o projeto criar automaticamente o schema de todas as tabelas e replicagem de dados(SEED).
    ex: "AppConnString": "Server=localhost\\SEUSQLSERVER;Database=SGM.Cidadao;Trusted_Connection=True;MultipleActiveResultSets=true"

## Como usar
* Apenas execute o VS Studio com todas as APIS simultaneamente.
