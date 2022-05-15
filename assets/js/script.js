
window.addEventListener('scroll',function(){
    let header=document.querySelector('header');
    if(window.pageYOffset>200){
        header.classList.add('sticky');
    }else{
        header.classList.remove('sticky');
    }
  
})







let navbar=document.querySelector('.header .navbar');
document.querySelector('#menu-btn').onclick=()=>{
    navbar.classList.add('active');
}
document.querySelector('#nav-close').onclick=()=>{
    navbar.classList.remove('active');
}


let searchForm=document.querySelector('.search-form');
document.querySelector('#search-btn').onclick=()=>{
    searchForm.classList.add('active');
}
document.querySelector('#close-search').onclick=()=>{
    searchForm.classList.remove('active');
}

window.onscroll=()=>{
    navbar.classList.remove('active')
}

//sliderpart
const btns=document.querySelectorAll(".nav-btn");
const sliders=document.querySelectorAll(".video");
const contents=document.querySelectorAll(".content");


var sliderNav=function(manual){
    btns.forEach((btn)=>{
        btn.classList.remove("active");
    });

    sliders.forEach((slide)=>{
        slide.classList.remove("active");
    });

    contents.forEach((content)=>{
        content.classList.remove("active");
    });

    btns[manual].classList.add("active");
    sliders[manual].classList.add("active");
    contents[manual].classList.add("active");


}
btns.forEach((btn,i) => {
    btn.addEventListener("click",()=>{
        sliderNav(i)
    })
});


//countdown

(function () {
    const second = 1000,
          minute = second * 60,
          hour = minute * 60,
          day = hour * 24;
  
    //I'm adding this section so I don't have to keep updating this pen every year :-)
    //remove this if you don't need it
    let today = new Date(),
        dd = String(today.getDate()).padStart(2, "0"),
        mm = String(today.getMonth() + 1).padStart(2, "0"),
        yyyy = today.getFullYear(),
        nextYear = yyyy + 1,
        dayMonth = "09/30/",
        birthday = dayMonth + yyyy;
    
    today = mm + "/" + dd + "/" + yyyy;
    if (today > birthday) {
      birthday = dayMonth + nextYear;
    }
    //end
    
    const countDown = new Date(birthday).getTime(),
        x = setInterval(function() {    
  
          const now = new Date().getTime(),
                distance = countDown - now;
  
          document.getElementById("days").innerText = Math.floor(distance / (day)),
            document.getElementById("hours").innerText = Math.floor((distance % (day)) / (hour)),
            document.getElementById("minutes").innerText = Math.floor((distance % (hour)) / (minute)),
            document.getElementById("seconds").innerText = Math.floor((distance % (minute)) / second);
  
          //do something later when date is reached
          if (distance < 0) {
            document.getElementById("headline").innerText = "It's my birthday!";
            document.getElementById("countdown").style.display = "none";
            document.getElementById("content").style.display = "block";
            clearInterval(x);
          }
          //seconds
        }, 0)
    }());


    //testiomal
  
    $(document).ready(function(){
        $("#testimonial-slider").owlCarousel({
            items:1,
            itemsDesktop:[1000,1],
            itemsDesktopSmall:[979,1],
            itemsTablet:[768,1],
            pagination:true,
            transitionStyle:"backSlide",
            autoPlay:true
        });
    });

   





     
