# .NET JWT auth service

### Run in container

```
docker build -t auth-service .

docker run -d --name=auth-service --env ConnectionStrings__Postgres="Host=postgres;Port=5432;Database=auth;User Id=postgres;Password=pwd12345;" --env "Auth__SecurityKey"="6f089df4-bd05-49e3-8535-cbdac42ddf47" -p 127.0.0.1:5000:80 auth-service
```