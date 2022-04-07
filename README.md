## **Eparafia**

Tutaj masz wszystko co musisz wiedzieć o api


## Endpoints Documentation
<b>User</b>
<hr>
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
```json
{
  "email": "string",
  "password": "string"
}
```
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
<hr>
<b>Priest</b>
<hr>
/priest/login

Logowanie na konto księdza 

```json
{
  "email": "string",
  "password": "string"
}
```

<hr>

/priest/isUserExist

Sprawdzasz czy istnieje po Id

```json
{
  "id": 0
}
```
<hr>

/priest/firstLogin

Tu masz pierwsze logowanie księdza z konfiguracją

```json
{
  "yearOfOrdination": 0,
  "password": "string",
  "email": "string",
  "fristLoginToken": "string"
}
```
<hr> 

<b>Parafia</b>
<hr>

/parafia/register

Zakładanie parafii 

```json
{
  "name": "string",
  "city": "string",
  "address": "string",
  "subscriptionExpiration": "string",
  "subscriptionPrice": 0,
  "priests": [
    {
      "name": "string",
      "surName": "string",
      "email": "string",
      "phoneNumber": "string"
    }
  ]
}
```

<hr> 

/parafia/get 

Pobierasz parafię w sensie zwykły getter 

```json
{
  "id": 0
}
```
<hr>

<b>Announcements</b>

<hr>

/announcements/add

Dodajesz ogłoszenia jako ks z jego konta

```json
{
  "parafiaId": 0,
  "title": "string",
  "content": "string"
}
```

<hr> 

/announcements/edit
 
 Księdzunio sie pomyli to ma szanse naprawić

```json
{
  "id": 0,
  "mode": 0,
  "content": "string"
}
```

```
mode:
	//0 - Content
	//1 - Tittle
```
<hr>

/announcements/get

Pobieranie listy ogłoszeń 
```json
{
  "parafiaId": 0,
  "page": 0
}
```



