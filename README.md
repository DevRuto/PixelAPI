# PixelAPI

![Build](https://github.com/devruto/pixelapi/workflows/.NET%20Core/badge.svg)

> An ASP.Net Core project intended to save and serve GOKZ replays (CSGO KZ Speedrun Mod)

---
**IMPORTANT: MySQL size limits**

> This is important regarding replays, and MySQL's `max_allowed_packet` property
* MySQL 8.0
    - Default `max_allowed_packet` size: **64MB**
    - This should be more than enough for replays, unless you somehow have a replay lasting several hours
    - [Source](https://dev.mysql.com/doc/refman/8.0/en/packet-too-large.html)
* MySQL 5.7 and below
    - Default `max_allowed_packet` size: **4MB**
    - This may be a bit too small for some cases
    - You should increase the size by adding the following settings to your mysql options file (google it)
        ```ini
        [mysqld]
        max_allowed_packet=64M
        ```
    - [Source](https://dev.mysql.com/doc/refman/5.7/en/packet-too-large.html)
---

### Sourcemod plugin
https://github.com/DevRuto/PixelAPI-SM

### Web Interface (WIP) [Preview](https://devruto.github.io/PixelApp/)
https://github.com/DevRuto/PixelApp

### * [DOCS](./DOC.md)

## TODO
- [x] Queries
- [ ] Docker
- [ ] Tests