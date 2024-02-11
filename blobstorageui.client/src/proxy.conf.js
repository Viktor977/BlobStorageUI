const PROXY_CONFIG = [
  {
    context: [
      "/file",
    ],
    target: "https://localhost:7021",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
