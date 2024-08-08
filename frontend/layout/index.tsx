"use client";
import { useState } from "react";
import "./style.css";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { IoMenuSharp } from "react-icons/io5";
import {
  MdBusinessCenter,
  MdLocalAtm,
  MdOutlineArrowDropDown,
} from "react-icons/md";
import { FaHome } from "react-icons/fa";
import { FaServicestack } from "react-icons/fa6";

const Layout = ({ handleExpandParent }: any) => {
  const [isExpand, setIsExpand] = useState<boolean>(true);

  const handleExpand = () => {
    // remove active and visible when collapse
    const objSubMenuId = document.querySelectorAll(".sub-menu");
    const objMenuActiveId = document.querySelectorAll(".active");
    for (var i = 0; i < objSubMenuId.length; i++) {
      objSubMenuId[i].classList.remove("visible");
    }
    for (var i = 0; i < objMenuActiveId.length; i++) {
      objMenuActiveId[i].classList.remove("active");
    }
    setIsExpand(!isExpand);
    handleExpandParent(!isExpand);
  };
  const [lstMenu, setLstMenu] = useState([
    {
      nMenuID: 1,
      sMenuName: "Home",
      nHeadMenu: null,
      sIcon: <FaHome />,
      sURL: "/",
      lstSubMenu: [],
    },
    {
      nMenuID: 2,
      sMenuName: "บริการ",
      nHeadMenu: null,
      sIcon: <FaServicestack />,
      sURL: "/",
      lstSubMenu: [
        {
          nMenuID: 3,
          sMenuName: "ฝากเงิน",
          nHeadMenu: 2,
          sIcon: "",
          sURL: "/",
          lstSubMenu: [],
        },
        {
          nMenuID: 4,
          sMenuName: "ถอนเงิน",
          nHeadMenu: 2,
          sIcon: "",
          sURL: "/",
          lstSubMenu: [],
        },
      ],
    },
    {
      nMenuID: 5,
      sMenuName: "งานบริการลูกค้า",
      nHeadMenu: null,
      sIcon: <MdBusinessCenter />,
      sURL: "/",
      lstSubMenu: [
        {
          nMenuID: 6,
          sMenuName: "เปิดบัญชี",
          nHeadMenu: 5,
          sIcon: "",
          sURL: "/",
          lstSubMenu: [],
        },
        {
          nMenuID: 7,
          sMenuName: "ประวัติการทำรายการ",
          nHeadMenu: 5,
          sIcon: "",
          sURL: "/",
          lstSubMenu: [],
        },
      ],
    },
    {
      nMenuID: 8,
      sMenuName: "ATM",
      nHeadMenu: null,
      sIcon: <MdLocalAtm />,
      sURL: "/",
      lstSubMenu: [
        {
          nMenuID: 9,
          sMenuName: "เติมเงิน",
          nHeadMenu: 8,
          sIcon: "",
          sURL: "/",
          lstSubMenu: [],
        },
        {
          nMenuID: 10,
          sMenuName: "ถอนเงิน",
          nHeadMenu: 8,
          sIcon: "",
          sURL: "/",
          lstSubMenu: [],
        },
        {
          nMenuID: 11,
          sMenuName: "ประวัติการทำรายการ",
          nHeadMenu: 8,
          sIcon: "",
          sURL: "/",
          lstSubMenu: [],
        },
      ],
    },
  ]);

  const pathname = usePathname().substring(1);
  const HandleSubMenu = (e: any, lstSubMenu: any) => {
    const sHeadMenuId = document.getElementById(e.target.id);
    lstSubMenu.forEach((objSubMenu: any) => {
      const sSubMenuId = document.getElementById(objSubMenu.nMenuID);
      if (sSubMenuId?.classList[1] !== "visible") {
        // click submenu and expand in the same time
        setIsExpand(true);
        // expand section
        handleExpandParent(true);
        sSubMenuId?.classList.add("visible");
        sHeadMenuId?.classList.add("active");
      } else {
        sSubMenuId?.classList.remove("visible");
        sHeadMenuId?.classList.remove("active");
      }
    });
  };

  const RenderLinkMenu = (objMenu: any, index: any) => {
    const isSub = objMenu.nHeadMenu != null ? true : false;
    return (
      <Link
        id={objMenu.nMenuID}
        href={objMenu.sURL}
        key={index}
        className={`${isSub ? "sub-menu" : "head-menu"}`}
      >
        <li>
          {objMenu.sIcon}
          <span className={`${isExpand ? "" : "collapse"}`}>
            {" "}
            {objMenu.sMenuName}
          </span>
        </li>
      </Link>
    );
  };

  const handleLogout = () => {
    const dropdownContentId = document.getElementById("dropdown-content");
    const profileId = document.getElementById("profile");

    if (dropdownContentId?.classList.length === 1) {
      dropdownContentId?.classList.add("visible");
      profileId?.classList.add("active");
    } else {
      dropdownContentId?.classList.remove("visible");
      profileId?.classList.remove("active");
    }
  };

  return (
    <>
      <header className={`${isExpand ? "" : "collapse"}`}>
        <h3>{pathname}</h3>
        <span className="profile" id="profile" onClick={handleLogout}>
          <img className="profile-pic" alt="profile-pic" src="profile.jpg" />
          <p>Firstname Lastname</p>
          <MdOutlineArrowDropDown />
          <div id="dropdown-content" className="dropdown-content">
            <p>ชื่อ: Prefix Firstname Lastname</p>
            <p>ตำแหน่ง: RoleName</p>
            <button className="logout-btn">Logout</button>
          </div>
        </span>
      </header>

      <nav className={`${isExpand ? "" : "collapse"}`}>
        <img className="logo" alt="favicon.ico" src="favicon.ico" />
        <button onClick={handleExpand} className="btn-collapse">
          <IoMenuSharp />
        </button>
        <menu>
          {lstMenu.map((objMenu, index) => {
            return (
              <div key={index}>
                {objMenu.lstSubMenu.length == 0 ? (
                  RenderLinkMenu(objMenu, objMenu.nMenuID)
                ) : (
                  <>
                    <li
                      key={objMenu.nMenuID}
                      id={"" + objMenu.nMenuID}
                      onClick={(e) => HandleSubMenu(e, objMenu.lstSubMenu)}
                    >
                      {objMenu.sIcon}
                      <span className={`${isExpand ? "" : "collapse"}`}>
                        {" "}
                        {objMenu.sMenuName}
                      </span>
                    </li>
                    {objMenu.lstSubMenu.map((objSubMenu) => {
                      return RenderLinkMenu(objSubMenu, objSubMenu.nMenuID);
                    })}
                  </>
                )}
              </div>
            );
          })}
        </menu>
      </nav>
    </>
  );
};

export default Layout;
