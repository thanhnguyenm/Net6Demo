require('dotenv').config();
const ProgressBar = require('progress')

let car = require('./model')

console.log(process.env.USER_ID) // "239482"
console.log(process.env.USER_KEY) // "foobar"
console.log(process.env.NODE_ENV) // "development"

console.log(car);

const bar = new ProgressBar(':bar', { total: 10 })
const timer = setInterval(() => {
  bar.tick()
  if (bar.complete) {
    clearInterval(timer)
  }
}, 100)
