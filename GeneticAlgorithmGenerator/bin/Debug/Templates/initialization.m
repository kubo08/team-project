% Vseobecny PGA 
%==================================================
clear
% Parametre PGA
% pocet generacii
numgen=300;         
% pocet retazcov v kazdej subpopulacii
lpop=30;            
% pocet subpopulacii
numpop=8;           
% pocet jedincov v retazci  
lret=10;            
% rozsah min
spacemin = -500;    
% rozsah max
spacemax = 500;     

SpaceAll=[ones(1,lret)*(spacemin);ones(1,lret)*spacemax]; 	% rozsah hladania riesenia

fitnesname='schwef';
fitnesparam=[];

% Parametre Migracie
typemigration=1;
periodmigration=50;
nummigration=2;
mumreinit=5;

% Inicializacia populacie a nastavenie parametrov pre jednotlive GA 
Fittrend=cell(1,numpop);
for k=1:numpop,  
    Pop{k}=genrpop(lpop,SpaceAll);	% generating of an initial population
    PopParam{k}=[0.1 0.05 0.2];     % [rozsah mutacie v % z celkoveho rozsahu, miera vseobecnej mutacie, miera aditivnej mutacie] 
    Fittrend{k}=[];
end

Fittrendnew=cell(1,numpop);
Fit=cell(1,numpop);
NewPop=cell(1,numpop);
gen=0;