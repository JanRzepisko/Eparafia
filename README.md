## **Eparafia**

Tutaj masz wszystko co musisz wiedzieć o api


## Endpoints Documentation
/user/register

Rejestracja Użytkownika

```json
{
  "name": "string",
  "surName": "string",
  "email": "string",
  "phoneNumber": "string",
  "password": "string"
}
```
<hr>
/user/login

Logowanie użytkownika 
> {
```json
{
  "email": "string",
  "password": "string"
}
```
}

<hr>

/user/isUserExist

Tu Sprawdzasz czy user istnieje jeżeli tak

```json
{
  "id": 0
}
```

<hr>

/user/settings

ustawienia zmiana wartości

```json
{
  "id": 0,
  "mode": 0,
  "value": "string"
}
```

