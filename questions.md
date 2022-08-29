#Questions
- Do you envision any situation where exempted vehicles vehicles become toll paying? or even  is that varying per city as well? and are they always exempted or can it vary per date?
- Are toll free days like weekends and holidays varying per city?
- Could a normal toll free date be conditionally or ocasionally deactivated?
- Could a normall toll paying day be treated different for instance, apply a discount percentage for the day?
- What does "You may limit the scope to the year 2013" mean? why is this important? Does it mean consider only calculating for this year? upto this year? or from this year ahead? Are there different rules for years before or after?
- Are taxation parameters going to evolve per period? That is, do we envision a situation where tax parameters are configured for a particular perdiod of time? In this case, the calculation would load those specific parameters for the period being calculated? This would help in cases where the calculation is done back in time and we want the results compatible with the rules of such period. At the moment, all calculations are done with the most upto date rules.
- When the endpoint is called are the input toll entry dates always sorted correctly?
- Could the max charge per day be for any other period like max charge per week or month? In this case, when calculating a charge of the day, the system would have to be aware of the current total per week/month and default to a set value?
- Is the single charge rule changing the default value? is it always in minutes?

Others
- How often is this endpoint called?
- Is this to be considered production ready solution? 
- How much restructuring of the code should be considered?

