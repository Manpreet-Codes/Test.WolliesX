

This is code based on ASP.net core 3.1.

This code does not have any Front End Implementation.

It has got three end points which is hosted on Azure 

https://answersapi.azurewebsites.net/api/

1. api\User - this is just the dummy implementation of Identity provider nothing really significant
			It just retuns some hardcoded values in Json
			
2. api\Sort?sortoption=**** - this is a GET API end point does following to test my ability to write high quality code for APIs and Unit Tests 
							1. access the wooliesX data provider api end-point 
							http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/products?token=eb848457-3d00-454f-9270-4490790cea30
								
							2. Deserialize it in the Object
							3. Sort the Object provided on the basis of price, name and recommended.
							4. For Recommended we are accessing another API endpoint for shopping history
								http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/shopperHistory?token=eb848457-3d00-454f-9270-4490790cea30
							5. We calculate the products by popularity (numbers sold) and sort the Products list and return
							
3. api\trolleytotal -- this is POST API endpoint, this is little hard to explain this tests my ability to understand application behaviour
					by observation and design a system to perform according to varied situations
					
					This is the input to the APIs - it has Product List, Quantity and Offers we need to implement in a way that it has the lowest
					possible price and return total.
					
					{
					  "products": [
						{
						  "name": "string",
						  "price": 0
						}
					  ],
					  "specials": [
						{
						  "quantities": [
							{
							  "name": "string",
							  "quantity": 0
							}
						  ],
						  "total": 0
						}
					  ],
					  "quantities": [
						{
						  "name": "string",
						  "quantity": 0
						}
					  ]
					}



