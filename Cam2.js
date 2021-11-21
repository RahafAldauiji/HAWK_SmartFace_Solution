Stream = require('node-rtsp-stream')
stream = new Stream({
    name: 'name',
    streamUrl: 'rtsp://admin:Admin12!@192.168.100.64:554/Streaming/channels/101',
    wsPort: 9901,
    ffmpegOptions: { // options ffmpeg flags
        '-stats': '', // an option with no neccessary value uses a blank string
        '-r': 30 // options with required values specify the value after the key
    }
})