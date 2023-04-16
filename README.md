# CheckByStopBase

## Dependencies

- PostgreSql
- SFTP

## Environment variables
| Name | Description |
|------|-------------|
| ConnectionStrings:main | The connection string to the service database |
| CompanyParserConfiguration:RetryMinute | The time after which the service will check for a new registry file |
| CompanySftpConfiguration:Host | SFTP Server Host | 
| CompanySftpConfiguration:UserName | SFTP user name |
| CompanySftpConfiguration:Password | Password for SFTP | 
| CompanySftpConfiguration:RemoteDirectory | The path to the folder where the Company registry files are located |

## Description of the service 
The service is designed to store the registry and, at the request of partners, checks whether the sent TaxNumbers are in the stop list.
If there are companies from the stop list among the sent TaxNumber, a report is generated in which all the companies found will be listed. Next, you can send this report to the manager's email or do something else with it

## Service Deployment
To deploy the service, you need to configure the env in the docker-compose file for yourself. Next, you need to raise the database using the docker command
```docker-compose -f docker-compose-local.yml up pg```. Then, you need to run the migrator, which initializes the raised database and creates all the necessary tables in it.
This can be done using the command ```docker-compose -f docker-compose-local.yml up -d check-by-stopbase-service-migrator```.
Next, you need to raise the SFTP server, in which the ```/data/CompanyRegistry``` folder will already be created.
You need to put the ```Company registry``` in this folder.
The example registry is located in the root folder of the project called ```CompanyRegistry.csv```.
After all of the above, you need to launch the service Api using the command ```docker-compose -f docker-compose-local.yml up -d check-by-stopbase-service-api```
The service is ready to work!