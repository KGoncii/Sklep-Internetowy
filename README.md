<img src="https://www.agh.edu.pl/home/ckim/multimedia/znak_graficzny/znak_nazwa_symetr/agh_nzw_s_pl_1w_wbr_rgb_150ppi.jpg"/>
<h2> Sklep Internetowy </h2>
<h3>Przyczyna</h3> Projektujemy oprogramowanie sklepu internetowego na ćwiczenia z Projektowania Oprogramowania.
<h3>Kompilator</h3> Wykorzystamy do kodowania program <b>Visual Studio 2019</b>, który został nam udostępniony przez naszą uczelnię <b><mark>AGH</mark></b> (niestety nie chce ona nam zapewnić wtyczki <mark>.NET</mark>), a także, we własnym zakresie <b>Visual Studio 2022</b>.
<h3>Diagram</h3> Do zaprojektowania oprogramowania użyty został program opierający się na UML - <b>Visual Paradigm</b>, również udostępniony przez naszą Alma Mater. 
<h1>Uruchamianie aplikacji</h1>
    <h2>Wymagania</h2>
    <ul>
        <li><a href="https://dotnet.microsoft.com/download" target="_blank">.NET SDK</a></li>
    </ul>
    <h2>Jak uruchomić</h2>
    <ol>
        <li>Klonuj repozytorium:
            <pre><code>git clone https://github.com/KGoncii/Sklep-Internetowy.git</code></pre>
        </li>
        <li>Przejdź do folderu projektu:
            <pre><code>cd Sklep-Internetowy</code></pre>
        </li>
        <li>Przygotuj zależności:
            <pre><code>dotnet restore</code></pre>
        </li>
        <li>Uruchom aplikację:
            <pre><code>dotnet run</code></pre>
        </li>
    </ol>
<h2> Działanie sklepu </h2>
Sklep jest podzielony na dwie części, będzie miał widok użytkownika (<b>Klient</b>) i administratora (<b>Admin</b>).
<ul>
  <li><b>Klient</b> będzie mógł przeglądać i zamawiać produkty dostępne w sklepie, a także zarządzać swoim koszykiem.</li>
  <li><b>Admin</b> będzie miał możliwość wprowadzania i usuwania produktów z magazynu, edytowania ich specyfikacji, zarządzania wysyłką produktów i dokumentowania zakupu.</li>
</ul>

<h2> Co ciekawego dodamy </h2>
<ul>
  <li><b>Filtry</b> produktów w widoku przeglądania produktów;</li>
  <li><b>Saldo</b> pokazujące stan konta;</li>
  <li><b>UI</b> rozszerzenie wyglądu aplikacji;</li>
  <li><b>Raporty</b> Generowanie raportów sprzedaży;</li>
</ul>
<i>Uwaga: w naszym sklepie dopiero dokonanie płatności ściąga produkt z listy dostępnych, nie samo dodanie do koszyka</i>

