// 'use client'
// import { useState } from 'react';
// import './style.css'
// import Link from 'next/link'
// import { usePathname } from 'next/navigation'


// const RootLayout = ({
//   children,
// }: {
//   children: React.ReactNode
// }) => {
//   const [lstMenu, setLstMenu] = useState([
//     {nMenuID: 1, sMenuName:"Home", nHeadMenu:null, sIcon:"", sURL:"/", lstSubMenu:[]},
//     {nMenuID: 2, sMenuName:"บริการ", nHeadMenu:null, sIcon:"", sURL:"/", lstSubMenu:[
//       {nMenuID: 3, sMenuName:"ฝากเงิน", nHeadMenu:2, sIcon:"", sURL:"/", lstSubMenu:[]},
//       {nMenuID: 4, sMenuName:"ถอนเงิน", nHeadMenu:2, sIcon:"", sURL:"/", lstSubMenu:[]}]
//     },
//     {nMenuID: 5, sMenuName:"งานบริการลูกค้า", nHeadMenu:null, sIcon:"", sURL:"/", lstSubMenu:[
//       {nMenuID: 6, sMenuName:"เปิดบัญชี", nHeadMenu:5, sIcon:"", sURL:"/", lstSubMenu:[]},
//       {nMenuID: 7, sMenuName:"ประวัติการทำรายการ", nHeadMenu:5, sIcon:"", sURL:"/", lstSubMenu:[]},]
//     },
//     {nMenuID: 8, sMenuName:"ATM", nHeadMenu:null, sIcon:"", sURL:"/", lstSubMenu:[
//       {nMenuID: 9, sMenuName:"เติมเงิน", nHeadMenu:8, sIcon:"", sURL:"/", lstSubMenu:[]},
//       {nMenuID: 10, sMenuName:"ถอนเงิน", nHeadMenu:8, sIcon:"", sURL:"/", lstSubMenu:[]},
//       {nMenuID: 11, sMenuName:"ประวัติการทำรายการ", nHeadMenu:8, sIcon:"", sURL:"/", lstSubMenu:[]}]
//     },
//   ])

//   const pathname = usePathname().substring(1)
//   const [isExpand, setIsExpand] = useState(true)

//   const HandleSubMenu = (e:any, lstSubMenu:any) => {
//     const sHeadMenuId = document.getElementById(e.target.id)
//     lstSubMenu.forEach((objSubMenu:any) => {
//       const sSubMenuId = document.getElementById(objSubMenu.nMenuID)
//       if(sSubMenuId?.classList[1] !== "visible"){
//         sSubMenuId?.classList.add("visible")
//         sHeadMenuId?.classList.add("active")
//       }else{
//         sSubMenuId?.classList.remove("visible")
//         sHeadMenuId?.classList.remove("active")
//       }
//     });
//   };

//   const handleExpand = () => {
//     setIsExpand(!isExpand)
//   }

//   const RenderLinkMenu = (objMenu:any, index:any) => {
//     const isSub = objMenu.nHeadMenu != null ? true : false    
//     return (
//       <Link id={objMenu.nMenuID} href={objMenu.sURL} key={index} className={`${isSub ? 'sub-menu' : ''}`}>
//         <li>{objMenu.sIcon}<span className={`${isExpand ? '' : 'collapse'}`}> {objMenu.sMenuName}</span></li>
//       </Link>
//     )
//   }

//   const handleLogout = (e:any) => {
//     const dropdownContentId = document.getElementById("dropdown-content")
//     console.log(dropdownContentId);
    
//     // if(dropdownContentId.classList[0])
//   }
//   const RenderLogout = () => {
//     return (
//       <div className='logout-container'>

//       </div>
//     )
//   }

//   return (
//     <html lang="en">
//       <body>
//         <header className={`${isExpand ? '' : 'collapse'}`}>
//           <h3>{pathname}</h3>
//           <span className='profile dropdown' >
//             <img className='profile-pic' alt='favicon.ico' src="favicon.ico" />
//             <div id="dropdown-content">
//               <div className="desc">Beautiful Cinque Terre</div>
//             </div>
//             <p>firstname lastname</p> 
//             {RenderLogout()}
//           </span>
//         </header>

//         <nav className={`${isExpand ? '' : 'collapse'}`}>
//           <img className='logo' alt='favicon.ico' src="favicon.ico" />
//           <menu>
//             {
//               lstMenu.map((objMenu, index)=>{
//                 return (
//                   <div key={index}>
//                     {
//                       objMenu.lstSubMenu.length == 0 ?
//                         RenderLinkMenu(objMenu, objMenu.nMenuID)
//                       :
//                         <>
//                           <li key={objMenu.nMenuID} id={''+objMenu.nMenuID} onClick={(e) => HandleSubMenu(e, objMenu.lstSubMenu)}>
//                             {objMenu.sIcon}<span className={`${isExpand ? '' : 'collapse'}`}> {objMenu.sMenuName}</span>
//                           </li>
//                           {
//                           (objMenu.lstSubMenu).map((objSubMenu) => {
//                               return (
//                                 RenderLinkMenu(objSubMenu, objSubMenu.nMenuID)
//                               )
//                             })
//                           }
//                         </>
//                     }
//                   </div>
//                 )
//               })
//             }
//             <button onClick={handleExpand}>เปิดปิด</button>
//           </menu>
//         </nav>
        
//         <section className={`${isExpand ? '' : 'collapse'}`}>
//           <div className="container">
//             {children}
//           </div>
//         </section>
//       </body>
//     </html>
//   )
// }

// export default RootLayout
"use client"
import Layout from '@/layout'
import React, { useState } from 'react'

const RootLayout = ({
  children,
}: {
  children: React.ReactNode
}) => {
  const [isExpand, setIsExpand] = useState(true)

  const handleExpand = (data:boolean) => {
    setIsExpand(data)
  }
  return (
    // <RootLayout />
    <html lang="en" className="html font-normal">
      <body>
        <Layout handleExpandParent={handleExpand}/>
        <section className={`${isExpand ? '' : 'collapse'}`}>
        {/* <section className='collapse'> */}
          <div className="container">
            {children}
          </div>
        </section>
      </body>
    </html>
  )
}

export default RootLayout
