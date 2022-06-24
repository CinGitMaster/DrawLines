Markup 


In your project a selfsigned certificate could be created with more CN (Commonname) and other attributes.

The procedure is execute this command, and fillin the appropriate fields.

PS C:\Users\Saties-CinnovaBV> New-SelfSignedCertificate `
>> -Type Custom `
>> -KeyUsage DigitalSignature `
>> -CertStoreLocation "Cert:\CurrentUser\My" `
>> -Subject "CN=Drawlines2 Copyright 2022 Cinnova Certificate" `
>> -TextExtension @( `
>>   "2.5.29.37={text}1.3.6.1.5.5.7.3.3,1.3.6.1.4.1.311.84.3.1", `
>>   "2.5.29.19={text}false")

2)  Run MMC and there in My > personal certificates you will see your certificate
3) In Visual studio in packages you can select a certificate from the store instead a file. Select the correct one you want to apply.
   Your certificate is recongised by the CN

 
======== 
1) Publishen en Installeren van UWP applicatie doe je als volgt:
Kies in Visual studio publish > Create App -> kies Siteloading type

2) Na Publish kunnen er 2 directories aangemaakt worden.
D:\Projects\SourceTree\Drawlines2\Drawlines2\Drawlines2\bin\pacagedist
en
D:\Projects\SourceTree\Drawlines2\Drawlines2 (Package)\AppPackages\Drawlines2 (Package)_1.1.8.0_Debug_Test\

Hier in dit voorbeeld is "D:\Projects\SourceTree\Drawlines2" de broncode directory.
packagedist functioneert niet, je gebruikt de AppPackages\Drawlines2 (Package)_xx_yy_Debug_Test als distributie/setup folder.


De setup distributie directory ziet er zo uit.
\Drawlines2 (Package)\AppPackages\Drawlines2 (Package)_1.1.8.0_Debug_Test\
- Add-AppDevPackage.ps1
- Install.ps1
- Drawlines2 (Package)_1.1.8.0_x64_Debug.cer
- Drawlines2 (Package)_1.1.8.0_x64_Debug.msixbundle

3) Open een Powershell als administrator en kies powershell 
Installatie script uitvoeren gaat met
powershell  -ExecutionPolicy Bypass -File .\Install.ps1 

of .\Add-AppDevPackage.ps1  

Er worden aantal vragen gesteld kies hier Y op.
Daarna is de applicatiePackage.appxmanifest Extension Exe Alias is opgenomen dan kan het programma getart worden gelijk door 
in de bat of powershell de applicatie alias aan te roepen. In dit geval
	Drawlines2Alias.exe


====================
Procedure om een bat of powerscript te maken waarmee een UWP Windows applicatie start vanuit een command prompt of een ECutHMI knop.
https://www.addictivetips.com/windows-tips/open-uwp-apps-from-command-line-windows-10/

Heb je geen alias extension in je project, dan kan je applicatie gestart worden door de volgende procedure.


 1) Eerst de Package familyname te vinden
get-appxpackage > UWP_Apps_List.txt
Zoek naar Publisher. Notabene het is moeilijk te herkennen of het om programma “Drawlines2” gaat.
Wellicht moet je af en toe het path van Installation location openen. Daar staan wel indicaties van de exe en de naam van het programma.

InstallLocation   : C:\Program Files\WindowsApps\3c034f66-96d3-44ca-9688-5422bb273f81_1.1.8.0_x64__11sjhn85588jw

2) Copieer de PackageFamilyName


3)  Open het AppxManifest.xml in de installation location
Zoek naar Application Id. Copieer dit.

4) In bat / cmd of powershell zet de volgende regel
explorer.exe shell:appsFolder\PackageFamilyName!Application ID
 
Voor ons geval wordt dit explorer.exe shell:appsFolder\3c034f66-96d3-44ca-9688-5422bb273f81_11sjhn85588jw!App
Dit plaats je in powershell of je bat