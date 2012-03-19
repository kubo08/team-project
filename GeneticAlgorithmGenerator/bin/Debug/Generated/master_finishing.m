% Best solution
BestPop=[]; FitBest=[];
for indxPop=1:numpop,
    BestPop(indxPop,:)=Pop{indxPop}(1,:);
    FitBest(indxPop)=Fit{indxPop}(1);
end
B=selbest(BestPop,FitBest,[1])
retcolor='brgymckb';
figure
hold on
for indxPop=1:numpop,
    plot(Fittrend{indxPop},retcolor(indxPop));
end
xlabel('generation')
ylabel('F(x)')
hold off
