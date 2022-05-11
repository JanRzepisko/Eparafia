
## **Eparafia**

Tutaj masz wszystko co musisz wiedzieć o api i o projekcie 

Ogólnie to robimy system dla parafii, chcemy "zastąpić" tradycyjne strony, które często są nie udane

<hr>

## Features
  
  1. Logowanie Użytkowników, Dołączanie do Parafii
  2. Zakładanie Parafii, Dodawanie księży, Logowanie księży konfiguracja ich kont
  3. Announcements - Ogłoszenia ludzie dołączeni do parafii widzą 
  4. Calendar - kalendarz ludzie widzą coś w stylu jak na teams - detail screen 
  5. Kolęda - Zaproszenie od ludzi -> budowa mapy -> chodzą i odznaczają u kogo był
  6. E-Składka - Normalnie przelew, blik, upay cały
  
  <hr>

## Tokeny
Przy logowaniu dostajesz takie śmieszne coś na przykład:

```72H8VCSDV6_11075037_06-05-2022 16:11:55```

i tym się wszędzie weryfikujesz tam w endpoint documentation masz wszystko gdzie kiedy jak go podać. Jak jest zły lub jakiś inny go zastąpił to wtedy wywali:

```BadAccesToken```


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

  

Tu masz pierwsze logowanie księdza z konfiguracją, first token księdzu ma na emaila

   
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
  "token": "string",
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
  ],
  "contactPhoneNumber": "string",
  "defaultWeek": [
    {
      "type": 0,
      "duration": 0,
      "description": "string",
      "day": 0,
      "time": "string"
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

<hr>
<b>Calendar</b>
<hr>

/calendar/editDefault

Te deafultowe wydarzenie typu codzienne msze podajesz przy register parafii tu masz tylko takie edity

```json
{
  "token": "string",
  "parafiaId": 0,
  "defaultWeek": [
    {
      "type": 0,
      "duration": 0,
      "description": "string",
      "day": 0,
      "time": "string"
    }
  ]
}
```
<hr>
/calendar/addSpecial

Są deafultowe wydarzenia i te eventy takie co dodaje sobie ks żeby ludzie wiedzieli. Format daty do ugadania ale u mnie to wszystko jedno on sobie konvertnie sam jak coś dziwnego przyjdzie

```json
{
  "token": "string",
  "parafiaId": 0,
  "specialEvent": {
    "type": 0,
    "duration": 0,
    "description": "string",
    "date": "2022-05-11T18:42:26.699Z"
  }
}
```

<hr>
/calendar/editSpecial

No jak masz edita to wiadomo sobie edytujesz jak na przykład księdzu się pomyli

```json
{
  "token": "string",
  "eventId": 0,
  "editType": 0,
  "value": "string"
}
```

<hr>

/calendar/deleteSpecial

No to usuwasz sobie taki czerwony guzik tam daj 
id to id danego eventu który dostaniesz w get calendar

```json
{
  "token": "string",
  "id": 0
}
```

<hr>

/calendar/getCalendar

no to zwykłe get i masz to ładnie wyświetlić tylko

```json
{
  "token": "string",
  "parafiaId": 0,
  "week": 0
}
```