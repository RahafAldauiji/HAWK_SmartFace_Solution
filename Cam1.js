Stream = require('node-rtsp-stream')
stream = new Stream({
    name: 'name',
    streamUrl: 'rtsp://admin:Admin12!@192.168.1.65:554/Streaming/channels/101',
    wsPort: 9900,
    ffmpegOptions: { // options ffmpeg flags
        '-stats': '', // an option with no neccessary value uses a blank string
        '-r': 30 // options with required values specify the value after the key
    }
})


// const onvif = require('node-onvif');
//
// console.log('Start the discovery process.');
// // Find the ONVIF network cameras.
// // It will take about 3 seconds.
// onvif.startProbe().then((device_info_list) => {
//     console.log(device_info_list.length + ' devices were found.');
//     // Show the device name and the URL of the end point.
//     device_info_list.forEach((info) => {
//         console.log('- ' + info.urn);
//         console.log('  - ' + info.name);
//         console.log('  - ' + info.xaddrs[0]);
//     });
// }).catch((error) => {
//     console.error(error);
// });