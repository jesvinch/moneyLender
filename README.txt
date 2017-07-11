## MoneyLender
	* Simulates a Lending application where the requested loan amount
	  is lend from group of lenders.
	* Application shouldn't lend all the money from one lender to single client. 
	* Diversify the lending.
	* Should try and give the client the best possible loan as possible
	* The application will display the amount borrowed, amount to pay back, monthly
	  repayment over a 36 month period. 

**How to run**
	* Navigate to \MoneyLender\MoneyLender\bin\Debug
	* Place .csv file with marketdata in this folder. 
	* Run using the command e.g moneyLender.exe market.csv 1000
	* moneyLender.exe csvFile LoanAmount

**Lending process**
	* Lend from the pool of lenders.
	* Pick the lender with cheapest rate first. 
	* Split their cash into £10 chunks. 
	* For each lender, ensure you don't lend out more than 1% of their capital to single borrower.

**Assumptions**
	* Since we are borrowing from pool of lenders and each lender has different interest rate, I have only displayed the highest interest rate in the output. 
	* market.csv contains information about the lenders
	* NOTE: If .csv file doesn't follow the format similar to market.csv then the program won't be able to read that csv file. 

**CalculationServiceTests**
	* Compound interest calculations for both tests checked via www.thecalculatorsite.com by entering amount lend by each lender
	  and their interest rate against the totalRepaymentDue to ensure the calculations are correct.

**Program flow**
	* Once borrower enters a loan amount.
	* Read data from CSV file.
	* Validate input
		- Ensure the requestedLoanAmount > 1000 &&  < 15000
		- Ensure there are enough funds.
		- Ensure the requestedLoanAmount is % 100. 
	* Provided above validation passes
		- Attempt to borrow from pool of investers ensuring that each investor only lends 1% of their capital to single borrower
		- Inform user if there isn't enough funds. If there is, display quote. 

**Notes**
	* Have made use of CSVHelper library to read from CSV file. (https://joshclose.github.io/CsvHelper/#getting-started) 
	* I haven't made use of IOC container as I feel there isn't a major need for this solution.
	* I haven't made use of a mocking framework as I wanted to do integration tests on 'LendingService' which is the only class which has dependencies.