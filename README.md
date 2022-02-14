# Shifts-ETL
Resenje je konzolna aplikacija koja sluzi da u koracima mogu da se testiraju faze samog zadatka.

Imamo app.config file u kom se podesava connectionStrings ka bazi i endpoint na koji se kacimo sa RESTom. Podeseno je po specifikaciji zadatka tako da sa default doker okruzenjem bi trebalo sve da radi.

Tehnologije:
C# .net framework 4.8
RestSharp	105.2.3.0	- Rest client
Petapoco	5.1.306		- ORM
Npgsql		5.0.12.0	- DB com i ORM dependancy
log4net		2.0.14.0	- Logging
AutoMapper 	10.0.0.0	- Object mapping


Mini uputstvo za aplikaciju

Ponudjene opcije:
1. Get all shifts to memory 					- Kaci se putem REST-a na izlozenu putanju i povlaci sve generisane objekte. Posto imamo samo next i previous link dok god u response dobijamo next link automatski se poziva i appenduju se rezultati. Uzeo sam si za slobodu da "zakucam" uzitavanje podataka na njegov dozvoljeni max od 30 rekorda po pozivu. Ostavljena je u pozadini mogucnost da se izlozi metoda sa kojom mozemo da dirigujemo (start i limit) parametrima. Podaci se pune u memoriju. 

2. Store all shifts to database					- Pomocu AutoMapper-a sam upario modele i pomocu PetaPoca ih spustio u bazu. Na zalost tu sam imao dosta tehnickih poteskoca pa nije odradjeno bas kako bi trebalo ali sto bi se reklo "Radi" :)

3. Store all shifts to database (optimised) 	- Isto kao i prethodna tacka samo se iskoristio Parallel.ForEeach sa kojim su se performanse optimizovale sa ~15sec na ~5sec za 360 objekata (sa nested objektima).
4. Calculate defined KPIs						- Od KPI-ova sam resio total_number_of_paid_breaks, min_shift_length_in_hours i max_allowance_cost_14d pomocu sql upita. Mean shift algoritam ne znam, a nisam imao dovoljno vremena da se upoznam s tim. Insert odradjenih KPI-ova takodje se cuva u bazi. U slucaju ako je neki vec sacuvan skipuje se.
5. Clear all data in db (no turning back :))	- Opcija koja je meni pomogla u razvoju i testiranju a moze biti korisna ;)
0. Exit											- Izlaz i aplikacije

Note:
- Radjen je samo insert u bazu tako da uvek proverava da li record postoji u bazi, ako ne dodaje ga ako postoji skippuje
- Krenuo sam sa unit testovima, ali nekako mi se otrglo kontorli nedostatak vremena a visak problema sa samom tehnologijom i komponentama ciji dependancy-i su pomalo nemoguci :)
- Error handling nije bas najlepsi, ali se bar sve loguje

Nadam se da cete malo i uzivati u ovoj ludnici od app-a.

