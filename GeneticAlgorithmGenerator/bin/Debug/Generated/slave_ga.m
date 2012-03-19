tic
while gen<numgen,           % hlavny cyklus pre GA

    % volanie jednotlivych GA
    for indxPop=1:numpop,
        [NewPop{indxPop},Fittrendnew{indxPop},Fit{indxPop}]=ga_slave(Pop{indxPop},PopParam{k},fitnesname,periodmigration,SpaceAll); % genetic algorithm function 
        Fittrend{indxPop}=[Fittrend{indxPop} Fittrendnew{indxPop}];
    end
    
    % migracia tu treba urobit vetvenie programu pre rozne typy migracie
    % teraz nastavena: kazky s kazdym si vymeni 2 najlepsich  + sa prida 5
    % novych
    for indxPop=1:numpop,
        BestPop=[];
        for indxPop2=1:numpop,
            if indxPop~=indxPop2
                BestPop=[BestPop; NewPop{indxPop2}(1:nummigration,:)];
            end
        end
        Pop{indxPop}=[NewPop{indxPop}(1:(lpop-(numpop-1)*nummigration)-mumreinit,:); BestPop; genrpop(mumreinit,SpaceAll)];

    end
    gen=gen+periodmigration;
    disp([' generacia  ...... ' int2str(gen)])
end;  
toc
