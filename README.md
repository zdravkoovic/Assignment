### ThreadWorker Model

Zadatak zahteva prikaz napretka tri nezavisna thread-a, od kojih svaki može biti cancelovan posebno. Umesto da se sva ta logika gura u ViewModel, prirodno rešenje je da svaki thread bude zaseban objekat — entitet koji zna svoje stanje.

`ThreadWorker` enkapsulira sve što jedan thread treba da zna o sebi: koliko dugo traje, koliko je prošlo i da li je cancelovan. Progress se ne čuva kao vrednost već se uvek računa iz `Elapsed` i `Duration`, što znači da nema šanse da UI i model budu out of sync.

Timer je odvojen od logike progresa — `Elapsed` i `Duration` mogu se postaviti direktno, bez pokretanja timera. Ovo omogućava unit testiranje bez čekanja.


### LoadersViewModel

LoadersViewModel je centralno mesto za logiku Loaders funkcionalnosti. Drži tri `ThreadWorker` objekta i jedan `Timer` koji svake sekunde ažurira napredak svakog aktivnog thread-a.

Samim tim, ThreadWorker je čist model koji samo drži podatke, a ViewModel je taj koji zna kada i kako da ih menja. Zavisnost ide samo u jednom smeru.

`TotalProgress` se ne čuva kao vrednost već se uvek računa kao prosek aktivnih thread-ova. Ako su svi cancelovani, vraća 0.


### Tests

Da bih pokrenuo testove, bilo je potrebno ažurirati `MSTest.TestAdapter` paket sa verzije `2.2.10` na `4.2.3`.