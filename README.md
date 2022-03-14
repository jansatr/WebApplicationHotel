# WebApplicationHotel
Enne kasutamist:
Programm vajab andmebaasi. Tabelid saab ise luua skriptidest, mis on projektiga kaasas või siis kasutada demo andmebaasi koos andmetega, mis on saadetud e-mailiga.
Peale andmebaasi loomist, tuleb uuendada Web.config faili connectionString tägi enda baasi andmetega.

Programmi tööst:
Ilma sisse logimata saab vaadata vabu tube, kuid ei ole võimalik neid broneerida. 
On 2 kasutajarolli. Uue kasutaja registreerimisel luuakse alati piiratu õigustega kasutaja. Laiendatud õigustega kasutaja näeb kahte lisamenüüd (kõikide kasutajade andmed ja kõik 
aktiivsed broneeringud (broneeringud, mille väljaregistreerimskuupäev on tänasese kuupäevaga võrdne või suurem). Laiendatud õigustega kasutaja määrab User tabeli Role väljal olev 1. 
Vaikimisi on seal 0. 
Sisselogitud kasutaja näeb vabu tube soovitud peroodist, teha broneeringud, vaadata enda broneeringuid ja neid kustutada juhul kui on broneeringu alguse ajast rohkem kui 3 ööpäeva (72h). 
Broneeringu alguse kellaajaks on määratud kõige levinunum hotellidesse registreerimise aeg (kell 15:00). Vabade tubad loendis on võimalik sorteerida toa suuruse (kohtade) ja toa ühe 
öö hinna järgi. 
Laiendatud õigustega kasutaja (hotelli töötaja) saab, lisaks eelpool mainitud õiguste, näha ka kõiki kasutajaid ja kõiki broneeringuid. 
