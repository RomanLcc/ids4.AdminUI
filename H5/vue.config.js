module.exports = {
  lintOnSave: false,
  devServer: {
    // proxy: {
    //   '/connect': {
    //     target: 'http://192.168.103.251:6006'
    //   },
    //   '/base': {
    //     target: 'http://192.168.103.251:6006'
    //   }
    // }
     https: true,
    // host: 'localhost', // can be overwritten by process.env.HOST
    port: 8080, // can be overwritten by process.env.PORT, if port is in use, a free one will be determined
    proxy: {
      '/base': {
        target: 'https://localhost:44320'
      }
    },
    // setup: (app) => {    //解决post没响应的问题
    //   app.post('/public/**', function(req, res) {
    //    res.redirect(req.originalUrl); //重定向到对应路径
    //   });
    // }
  }
}