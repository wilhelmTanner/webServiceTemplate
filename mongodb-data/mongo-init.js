db.auth('usr_admin','admintanner')

db = db.getSiblingDB('catalogdb')

db.createUser({
    user: 'usr_test',
    pwd: 'tanner',
    roles: [
      {
        role: 'readWrite',
        db: 'catalogdb',
      },
    ],
  });

db.createCollection('example')

db.example.insertMany([
    { Name: "Item 01", Year: 2008, Active: true  },   
    { Name: "Item 02", Year: 2008, Active: true  },
    { Name: "Item 03", Year: 2008, Active: true  },    
    { Name: "Item 04", Year: 2008, Active: true  },
    { Name: "Item 05", Year: 2019, Active: true  },   
    { Name: "Item 06", Year: 2019, Active: true  },
    { Name: "Item 07", Year: 2019, Active: true  },    
    { Name: "Item 08", Year: 2019, Active: true  },
    { Name: "Item 09", Year: 2020, Active: true  },   
    { Name: "Item 10", Year: 2020, Active: true  },
    { Name: "Item 11", Year: 2020, Active: true  },    
    { Name: "Item 12", Year: 2020, Active: true  },
    { Name: "Item 13", Year: 2021, Active: true  },   
    { Name: "Item 14", Year: 2021, Active: true  },
    { Name: "Item 15", Year: 2021, Active: true  },    
    { Name: "Item 16", Year: 2021, Active: true  }	
 ])

