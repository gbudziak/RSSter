using System.Collections.Generic;
using System.Linq;
using Models.RSS;
using Models.User;
using Moq;
using NUnit.Framework;
using RssDataContext;
using Services.RssReader.Implementation;

namespace RSSter.Tests
{
    [TestFixture]
    public class ItemServiceTests
    {
        [SetUp]
        public void inti()
        {
            MapperConfig.Register();
        }

        [Test]
        [ExpectedException]
        public void T001_GetUserChannelItems_When_Not_Items_On_Channel()
        {
            // arrange
            var dbMock = new Mock<IApplicationRssDataContext>();
            var sut = new ItemService(dbMock.Object);

            var userChannel = new UserChannel(1, "1");
            userChannel.Id = 1;
            userChannel.UserItems = new List<UserItem>();
            userChannel.Channel = new Channel();

            var userChannels = new List<UserChannel> { userChannel }.AsQueryable();

            // arrange_mock
            dbMock.Setup(s => s.UserChannels.Provider).Returns(userChannels.Provider);
            dbMock.Setup(s => s.UserChannels.Expression).Returns(userChannels.Expression);
            dbMock.Setup(s => s.UserChannels.ElementType).Returns(userChannels.ElementType);
            dbMock.Setup(s => s.UserChannels.GetEnumerator()).Returns(userChannels.GetEnumerator());


            // act
            sut.GetUserChannelItems(1, "1", UserViewType.Custom1, 1, 1);

            // assert

            // assert_mock
        }

        [Test]
        public void T002_GetUserChannelItems_not_throwing_when_there_are_items_on_user_list()
        {
            // arrange
            var dbMock = new Mock<IApplicationRssDataContext>();
            var sut = new ItemService(dbMock.Object);

            var userChannel = new UserChannel(1, "1") {Id = 1, UserItems = new List<UserItem> {new UserItem()}};
            var channel = new Channel {Items = new List<Item>() {new Item()}};
            userChannel.Channel = channel;
            

            var userChannels = new List<UserChannel> { userChannel }.AsQueryable();

            // arrange_mock
            dbMock.Setup(s => s.UserChannels.Provider).Returns(userChannels.Provider);
            dbMock.Setup(s => s.UserChannels.Expression).Returns(userChannels.Expression);
            dbMock.Setup(s => s.UserChannels.ElementType).Returns(userChannels.ElementType);
            dbMock.Setup(s => s.UserChannels.GetEnumerator()).Returns(userChannels.GetEnumerator());


            // act
            sut.GetUserChannelItems(1, "1", UserViewType.Custom1, 1, 1);

            // assert

            // assert_mock
        }
        

        [Test]
        public void T004_not_workign_for_NotConstraint_existing()
        {
            // arrange
            var dbMock = new Mock<IApplicationRssDataContext>();
            var sut = new ItemService(dbMock.Object);

            var userChannel = new UserChannel(1, "1");
            userChannel.Id = 1;
            userChannel.UserItems = new List<UserItem> { new UserItem() };
            var channel = new Channel();
            channel.Items = new List<Item>() { new Item() };
            userChannel.Channel = channel;


            var userChannels = new List<UserChannel> { userChannel }.AsQueryable();

            // arrange_mock
            dbMock.Setup(s => s.UserChannels.Provider).Returns(userChannels.Provider);
            dbMock.Setup(s => s.UserChannels.Expression).Returns(userChannels.Expression);
            dbMock.Setup(s => s.UserChannels.ElementType).Returns(userChannels.ElementType);
            dbMock.Setup(s => s.UserChannels.GetEnumerator()).Returns(userChannels.GetEnumerator());


            // act
            sut.GetUserChannelItems(1, "10", UserViewType.Custom1, 10, 1000);

            // assert

            // assert_mock
        }


        [Test]
        public void T005_not_workign_for_not_existing_channel()
        {
            // arrange
            var dbMock = new Mock<IApplicationRssDataContext>();
            var sut = new ItemService(dbMock.Object);

            var userChannel = new UserChannel(1, "1");
            userChannel.Id = 1;
            userChannel.UserItems = new List<UserItem> { new UserItem() };
            var channel = new Channel();
            channel.Items = new List<Item>() { new Item() };
            userChannel.Channel = channel;


            var userChannels = new List<UserChannel> { userChannel }.AsQueryable();

            // arrange_mock
            dbMock.Setup(s => s.UserChannels.Provider).Returns(userChannels.Provider);
            dbMock.Setup(s => s.UserChannels.Expression).Returns(userChannels.Expression);
            dbMock.Setup(s => s.UserChannels.ElementType).Returns(userChannels.ElementType);
            dbMock.Setup(s => s.UserChannels.GetEnumerator()).Returns(userChannels.GetEnumerator());


            // act
            sut.GetUserChannelItems(10, "1", UserViewType.Custom1, 10, 1000);

            // assert

            // assert_mock
        }

        [Test]
        public void T005_workign_for_not_existing_channel()
        {
            // arrange
            var dbMock = new Mock<IApplicationRssDataContext>();
            var sut = new ItemService(dbMock.Object);

            var userChannel = new UserChannel(1, "1");
            userChannel.Id = 1;
            userChannel.UserItems = new List<UserItem> { new UserItem() };
            var channel = new Channel();
            channel.Items = new List<Item>() { new Item() };
            userChannel.Channel = channel;


            var userChannels = new List<UserChannel> { userChannel }.AsQueryable();

            // arrange_mock
            dbMock.Setup(s => s.UserChannels.Provider).Returns(userChannels.Provider);
            dbMock.Setup(s => s.UserChannels.Expression).Returns(userChannels.Expression);
            dbMock.Setup(s => s.UserChannels.ElementType).Returns(userChannels.ElementType);
            dbMock.Setup(s => s.UserChannels.GetEnumerator()).Returns(userChannels.GetEnumerator());


            // act
            sut.GetUserChannelItems(1, "1", UserViewType.Simple, 10, 1000);

            // assert

            // assert_mock
        }

    }
}