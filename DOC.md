# API

All endpoints has a `offset` and `limit` parameter

Interactive API documentation at `/api/swagger` in your browser

## Maps
---
### **GET** `/api/maps`
### *Response*
```json
{
  "offset": 0,
  "count": 2,
  "total": 2,
  "data": [
    {
      "name": "kz_11342",
      "created": "2020-08-30T16:15:51.984063"
    },
    {
      "name": "kz_baxter",
      "created": "2020-08-30T19:42:27.924232"
    }
  ]
}
```
---
## Players
---
### **GET** `/api/players`
### *Response*
```json
{
  "offset": 0,
  "count": 1,
  "total": 1,
  "data": [
    {
      "steamID64": "76561198120168778",
      "name": "Ruto",
      "created": "2020-08-30T16:13:45.087687",
      "updated": "2020-08-30T19:42:27.896416"
    }
  ]
}
```
---
## Records
---
### **GET** `/api/records`
### *Response*
```json
{
  "offset": 0,
  "count": 2,
  "total": 2,
  "data": [
    {
      "id": 2,
      "steamID64": "76561198120168778",
      "playerName": "Ruto",
      "map": "kz_11342",
      "mode": 1,
      "modeName": "SimpleKZ",
      "course": 0,
      "style": 0,
      "time": 160375,
      "teleports": 21,
      "created": "2020-08-30T16:15:52.130095"
    },
    {
      "id": 3,
      "steamID64": "76561198120168778",
      "playerName": "Ruto",
      "map": "kz_baxter",
      "mode": 1,
      "modeName": "SimpleKZ",
      "course": 1,
      "style": 0,
      "time": 2780,
      "teleports": 0,
      "created": "2020-08-30T19:42:28.076519"
    }
  ]
}
```
---
## Replays
---
### **POST** `/api/replay`
* ### **BODY**: GOKZ Replay binary
### *Successful Response*
```json
{
    "success": true,
    "id": 2
}
```
### *Failed Response*
```json
{
    "success": false,
    "id": -1
}
```
---
