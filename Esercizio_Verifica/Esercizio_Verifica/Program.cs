/* VERIFICA DI INFORMATICA
 * Autore: Francesco Di Lena
 * Classe: 4F
 * Data: 13/01/2022
 * Consegna: Scrivere il codice in C# per creare la classe Treni con i seguenti attributi: codtreno, tipo
            (regionale,nazionale,internazionale) ,nome e costo. La classe avrà 2 metodi che calcolano il costo del mezzo
            utilizzato e il costo per il suo utilizzo (calcolato dal percorso effettuato). Dalla classe si vogliono derivare 2
            differenti classi: Passeggeri e Merci che avranno 2 ulteriori attributi: numvagoni e alimentazione.
            Dopo aver visualizzato i dati (assegnati nel main o letti in input) calcolare e visualizzare:
            1) il costo dei 2 mezzi sapendo che il costo di un mezzo generico è 100.000€ mentre per il Passeggeri è
            aumentato del 25% e per il Merci aumenta del 35%;
            2) il costo per il suo utilizzo, dopo aver letto in input i km effettuati, sapendo che il prezzo al km per il
            treno Merci è di 100€ mentre per il Passeggeri è 150€;
            3) Il costo totale dei due mezzi.
*/

using System;

namespace Esercizio_Verifica
{
    //Classi

    class Program //La classe Program è la prima ad essere eseguita dal programma e contiene le istruzioni che consentono l'input e l'output delle informazioni che riguardano i treni.
    {
        //Attributi

        //Attributi di sola lettura, usati per la semplice visualizzazione di indicazioni all'utente, oppure costanti per il calcolo dei costi dei treni.

        static readonly string[] tipologiaTreno = new string[] { "passeggeri", "merci" };
        static readonly string[] caratteristicheTreno = new string[] { "il nome", "il tipo", "il codice", "il costo generico", "l'alimentazione", "il numero di vagoni", "il numero di km percorsi" };
        static readonly int[] percentualiAumento = new int[] { 25, 35 };
        static readonly int[] prezziKilometro = new int[] { 150, 100 };

        //Attributi "scrivibili"

        static string[] datiInseriti = new string[caratteristicheTreno.Length]; //Conterrà i dati inseriti dall'utente per ogni treno.
        static int totaleCostoTreni; //Conterrà il costo totale dei treni.

        //Metodi

        static void Main(string[] args) //Il metodo Main è il primo ad essere eseguito dal programma.
        {
            Console.WriteLine("Benvenuto nel programma!");
            /* Di seguito si trova un ciclo for che visualizza le indicazioni per l'utente e permette l'inserimento dei dati
             * riguardanti prima il treno passeggeri e poi il treno merci. Succesivamente visualizza le informazioni inserite
             * ed elaborate per ognuno dei due treni.
             */
            for (int i = 0; i < tipologiaTreno.Length; i++)
            {
                for (int j = 0; j < caratteristicheTreno.Length; j++)
                {
                    Console.WriteLine($"\nInserisci {caratteristicheTreno[j]} del treno {tipologiaTreno[i]}:");
                    datiInseriti[j] = Console.ReadLine();
                }
                switch (i)
                {
                    case 0:
                        Passeggeri TrenoPasseggeri = new Passeggeri(datiInseriti[0], datiInseriti[1], datiInseriti[2], int.Parse(datiInseriti[3]), datiInseriti[4], int.Parse(datiInseriti[5]));
                        totaleCostoTreni = TrenoPasseggeri.CalcolaCostoMezzo(percentualiAumento[i]) + TrenoPasseggeri.CalcolaCostoPercorso(int.Parse(datiInseriti[6]), prezziKilometro[i]);
                        Console.WriteLine($"\nIl primo treno è: {TrenoPasseggeri.ToString()}, con costo del mezzo specifico {TrenoPasseggeri.CalcolaCostoMezzo(percentualiAumento[i])} euro e costo del percorso di {TrenoPasseggeri.CalcolaCostoPercorso(int.Parse(datiInseriti[6]), prezziKilometro[i])} euro.\n");
                        break;
                    case 1:
                        Merci TrenoMerci = new Merci(datiInseriti[0], datiInseriti[1], datiInseriti[2], int.Parse(datiInseriti[3]), datiInseriti[4], int.Parse(datiInseriti[5]));
                        totaleCostoTreni += TrenoMerci.CalcolaCostoMezzo(percentualiAumento[i]) + TrenoMerci.CalcolaCostoPercorso(int.Parse(datiInseriti[6]), prezziKilometro[i]);
                        Console.WriteLine($"\nIl primo treno è: {TrenoMerci.ToString()}, con costo del mezzo specifico {TrenoMerci.CalcolaCostoMezzo(percentualiAumento[i])} euro e costo del percorso di {TrenoMerci.CalcolaCostoPercorso(int.Parse(datiInseriti[6]), prezziKilometro[i])} euro.\n");
                        break;
                }
            }
            Console.WriteLine($"Il costo totale, infine, dei treni sarà di {totaleCostoTreni} euro.");
            Console.WriteLine("Per uscire dal programma, premere un tasto qualsiasi...");
            Console.ReadKey(); //Attende un tipo qualsiasi di input da tastiere.
        }
    }

    class Treni //Classe padre.
    {
        //Attributi di tipo protected, che non sono visibili alle altre classi (eccetto alle figlie).

        protected string nome, tipo, codtreno;
        protected int costo;

        //Metodi

        /* Di seguito si trova il metodo costruttore, che permette di far corrispondere i parametri passati
         * agli attributi della classe.
         */

        public Treni(string nome, string tipo, string codtreno, int costo) 
        {
            this.nome = nome;
            this.tipo = tipo;
            this.codtreno = codtreno;
            this.costo = costo;
        }

        public virtual int CalcolaCostoMezzo(int percentualeAumento) //Metodo per il calcolo del costo di un mezzo generale che può essere sovrascritto.
        {
            return costo;
        }

        public virtual int CalcolaCostoPercorso(int kilometriPercorsi, int prezzoKilometro) //Metodo per il calcolo del costo di un percorso generale che può essere sovrascritto.
        {
            return 0;
        }

        /* Viene creato di seguito un nuovo metodo ToString(), sovrascrivendo totalmente quello originale, in modo da poter comporre le stringhe 
         * con gli attributi di un treno e visualizzarle direttamente sulla console.
        */

        new public virtual string ToString() 
        {
            return $"{nome}, tipo {tipo}, con codice {codtreno}, costo {costo} euro";
        }
    }

    class Passeggeri : Treni //Classe figlia.
    {
        //Attributi di tipo private, che sono visibili solo in questa classe.

        private string alimentazione;
        private int numvagoni;

        //Metodi

        /* Di seguito si trova il metodo costruttore, che permette di far corrispondere i parametri passati
         * agli attributi della classe, ereditandone una parte dalla classe padre.
         */

        public Passeggeri(string nome, string tipo, string codtreno, int costo, string alimentazione, int numvagoni) : base(nome, tipo, codtreno, costo)
        {
            this.alimentazione = alimentazione;
            this.numvagoni = numvagoni;
        }

        /* Di seguito si trova il metodo che sovrasrive l'omonimo metodo della classe padre
         * e calcola il costo specifico del treno passeggeri.
         */

        public override int CalcolaCostoMezzo(int percentualeAumento) 
        {
            return base.CalcolaCostoMezzo(percentualeAumento) + (base.CalcolaCostoMezzo(percentualeAumento) / 100 * percentualeAumento);
        }

        /* Di seguito si trova il metodo che sovrasrive l'omonimo metodo della classe padre
         * e calcola il costo specifico del percorso del treno passeggeri.
         */

        public override int CalcolaCostoPercorso(int kilometriPercorsi, int prezzoKilometro)
        {
            return prezzoKilometro * kilometriPercorsi;
        }

        /* Di seguito si trova il metodo che sovrasrive l'omonimo metodo della classe padre
         * e aggiunge ulteriori caratteristiche del treno passeggeri alla stringa di base.
         */

        public override string ToString()
        {
            return base.ToString() + $", alimentazione {alimentazione} con {numvagoni} vagoni";
        }
    }

    class Merci : Treni //Classe figlia.
    {
        //Attributi di tipo private, che sono visibili solo in questa classe.

        private string alimentazione;
        private int numvagoni;

        //Metodi

        /* Di seguito si trova il metodo costruttore, che permette di far corrispondere i parametri passati
         * agli attributi della classe, ereditandone una parte dalla classe padre.
         */

        public Merci(string nome, string tipo, string codtreno, int costo, string alimentazione, int numvagoni) : base(nome, tipo, codtreno, costo)
        {
            this.alimentazione = alimentazione;
            this.numvagoni = numvagoni;
        }

        /* Di seguito si trova il metodo che sovrasrive l'omonimo metodo della classe padre
         * e calcola il costo specifico del treno merci.
         */

        public override int CalcolaCostoMezzo(int percentualeAumento)
        {
            return base.CalcolaCostoMezzo(percentualeAumento) + (base.CalcolaCostoMezzo(percentualeAumento) / 100 * percentualeAumento);
        }

        /* Di seguito si trova il metodo che sovrasrive l'omonimo metodo della classe padre
         * e calcola il costo specifico del percorso del treno merci.
         */

        public override int CalcolaCostoPercorso(int kilometriPercorsi, int prezzoKilometro)
        {
            return prezzoKilometro * kilometriPercorsi;
        }

        /* Di seguito si trova il metodo che sovrasrive l'omonimo metodo della classe padre
         * e aggiunge ulteriori caratteristiche del treno passeggeri alla stringa di base.
         */

        public override string ToString()
        {
            return base.ToString() + $", alimentazione {alimentazione} con {numvagoni} vagoni";
        }
    }
}
