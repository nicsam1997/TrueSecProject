db = db.getSiblingDB('TrueSecDb');

db.createUser({
  user: 'truesec_user',
  pwd: 'truesec_password',
  roles: [
    {
      role: 'readWrite',
      db: 'TrueSecDb',
    },
  ],
});

db.createCollection("Vulnerabilities");
db.createCollection("AuthorizedUsers");
// TODO: Not production ready, use hashed password in the real application
db.AuthorizedUsers.insertMany([
  {
    _id: "admin",
    password: "AdminPassword123!",
    role: "Admin"
  },
  {
    _id: "user",
    password: "UserPassword123!",
    role: "User"
  }
]);