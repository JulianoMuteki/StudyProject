[![.NET](https://github.com/JulianoMuteki/StudyProject/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/JulianoMuteki/StudyProject/actions/workflows/dotnet.yml)


# Study Project
ASP.NET MVC Core + EF Core + NET 6

🇺🇸 This project is used as a study base to improve my technical skills. I'm modeling according to tutorials, videos and books.
It has still a lot of bugs but that's part of it. I'm open to suggestions, tips.

🇧🇷 Este projeto é usado como base de estudo para aperfeiçoar minhas habilidades técnicas. Estou modelando conforme tutoriais, vídeos e demos.
Ainda muitos bugs mas faz parte. Estou aberto a sugestões, dicas 😁

🇮🇹 Questo progetto viene utilizzato come base di studio per migliorare le mie capacità tecniche. Sto modellando secondo tutorial, video e libri.
Ha ancora molti bug, ma ne fa parte. Sono aperto a suggerimenti, consigli.

Stack project:
 - API ASP.NET Core
 - ASP.NET MVC Core
 - Fluent Validation
 - Entity Framework Core
 - IdentityUser
 - Swagger
 - RabbitMQ 
 - SQL Server 
 - Docker
 - Azure
 - Tests

What i want to learn: DDD + CQRS + SOLID
 
 References:
  - https://www.freecodecamp.org/
  - https://docs.microsoft.com/en-us/
  - https://stackoverflow.com/

# Instructions:
## Config USER SECRETS
 - Create folder with your code USER_SECRETS_ID in /mnt/c/Users/"YOUR_USER"/AppData/Roaming/Microsoft/UserSecrets/$USER_SECRETS_ID
 - Edit docker\secret.json with datas
 - Copy docker\secret.json to /mnt/c/Users/julia/AppData/Roaming/Microsoft/UserSecrets/$USER_SECRETS_ID
 - Add USER_SECRETS_ID code in docker\.env.dev

## Docker

docker-compose -f docker-compose.yaml -f docker-compose.dev.yaml --env-file .env.dev build
docker-compose -f docker-compose.yaml -f docker-compose.dev.yaml --env-file .env.dev up -d